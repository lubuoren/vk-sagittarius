"""hidraw 传输层：无第三方依赖，直接走 /sys + /dev/hidraw*。

Windows 端用 SetupAPI + CreateFile 枚举 HID 设备后按 VID/PID 筛选；
Linux 端等价物是 /sys/class/hidraw 下的 uevent（HID_ID=总线:VID:PID）。
"""

from __future__ import annotations

import errno
import fcntl
import os
import select
import struct
import time
from dataclasses import dataclass
from typing import Protocol

from .protocol import PID_CLASS, REPORT_ID, VID

HIDIOCGRAWNAME = 0x80804804
HIDIOCGRAWINFO = 0x80084803


class Transport(Protocol):
    cls: int
    index: int

    def write(self, report: bytes) -> None: ...
    def read(self, timeout: float = 0.0) -> bytes | None: ...
    def close(self) -> None: ...


@dataclass
class HidCandidate:
    path: str
    vid: int
    pid: int
    name: str

    @property
    def cls(self) -> int | None:
        return PID_CLASS.get(self.pid)


def _read_uevent(path: str) -> dict[str, str]:
    info: dict[str, str] = {}
    try:
        with open(path, "r", encoding="utf-8", errors="replace") as fh:
            for line in fh:
                if "=" in line:
                    k, v = line.strip().split("=", 1)
                    info[k] = v
    except OSError:
        pass
    return info


def scan() -> list[HidCandidate]:
    """枚举所有 hidraw 节点，返回 VID 匹配 0x0416 的候选。"""
    out: list[HidCandidate] = []
    root = "/sys/class/hidraw"
    if not os.path.isdir(root):
        return out
    for node in sorted(os.listdir(root)):
        uevent = _read_uevent(os.path.join(root, node, "device", "uevent"))
        hid_id = uevent.get("HID_ID", "")
        try:
            _bus, vid_s, pid_s = hid_id.split(":")
            vid, pid = int(vid_s, 16), int(pid_s, 16)
        except ValueError:
            continue
        if vid != VID:
            continue
        out.append(HidCandidate(path=f"/dev/{node}", vid=vid, pid=pid,
                                name=uevent.get("HID_NAME", "").strip()))
    return out


class HidrawTransport:
    """单个 hidraw 设备的读写封装。"""

    def __init__(self, path: str, cls: int, index: int = 0):
        self.path = path
        self.cls = cls
        self.index = index
        self.fd = os.open(path, os.O_RDWR | os.O_NONBLOCK)
        self._warned_perm = False

    # --- helpers
    def _name(self) -> str:
        try:
            buf = bytearray(64)
            fcntl.ioctl(self.fd, HIDIOCGRAWNAME, buf, True)
            return buf.split(b"\x00", 1)[0].decode("utf-8", "replace")
        except OSError:
            return ""

    def write(self, report: bytes) -> None:
        if not report or report[0] != REPORT_ID:
            report = bytes([REPORT_ID]) + report
        try:
            os.write(self.fd, report)
        except BlockingIOError:
            # hidraw 写缓冲满：让出一次时间片再试
            time.sleep(0.002)
            os.write(self.fd, report)

    def read(self, timeout: float = 0.0) -> bytes | None:
        r, _, _ = select.select([self.fd], [], [], timeout)
        if not r:
            return None
        try:
            data = os.read(self.fd, 64)
        except OSError as exc:
            if exc.errno in (errno.EAGAIN, errno.EWOULDBLOCK):
                return None
            raise
        return data or None

    def close(self) -> None:
        if self.fd >= 0:
            try:
                os.close(self.fd)
            finally:
                self.fd = -1


class FakeTransport:
    """内存假设备，供无硬件环境做端到端自检。"""

    def __init__(self, cls: int, index: int = 0, state: bytes | None = None):
        self.cls = cls
        self.index = index
        self.sent: list[bytes] = []
        self._queue: list[bytes] = []
        self._state = state

    def write(self, report: bytes) -> None:
        self.sent.append(bytes(report))
        # 模拟固件：收到 GET_STATE 就回报一份状态
        if len(report) > 12 and report[12] == 2:
            self._queue.append(self._state or self._default_state())

    def _default_state(self) -> bytes:
        buf = bytearray(65)
        buf[0] = 0
        buf[1] = 0xDC
        buf[2] = 0xDC
        buf[3] = 0x01
        buf[4] = self.cls
        buf[12] = 2          # GET_STATE 回应标记（含 ReportID 时 base=1）
        off = 1
        for ch in range(4):
            buf[off + 15 + ch * 3] = 2
            rpm = 800 + ch * 120
            struct.pack_into("<H", buf, off + 16 + ch * 3, rpm)
        buf[off + 27] = 6      # 灯效模式
        buf[off + 28] = 100    # 亮度
        buf[off + 29] = 1      # 速度
        buf[off + 30:off + 33] = bytes([255, 0, 0])
        return bytes(buf)

    def read(self, timeout: float = 0.0) -> bytes | None:
        return self._queue.pop(0) if self._queue else None

    def close(self) -> None:
        pass


def open_devices(prefer: Transport | None = None) -> list[Transport]:
    """按类别打开所有匹配设备，同类多设备按序号排列。"""
    if prefer is not None:
        return [prefer]
    found = [c for c in scan() if c.cls is not None]
    counters: dict[int, int] = {}
    devs: list[Transport] = []
    for cand in found:
        cls = cand.cls
        idx = counters.get(cls, 0)
        counters[cls] = idx + 1
        try:
            devs.append(HidrawTransport(cand.path, cls, idx))
        except PermissionError:
            raise PermissionError(
                f"无权访问 {cand.path}：安装 udev/99-vk-sagittarius.rules 或把用户加入相应权限组"
            ) from None
    return devs
