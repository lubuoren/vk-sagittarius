"""VK 射手座控制中心 —— HID 协议层（纯逻辑，不碰硬件）。

全部常量与字节布局来自对 VAIKYRIE.exe / UsbHid.dll 的反编译结果：

    CommandMessage     -> 输出报文固定 65 字节：[0]=ReportID(0) + 64 字节数据
    UserDevice.send*   -> 头部 14 字节 + 负载（<=50 字节）

头部布局（偏移为「不含 ReportID」的数据区偏移，即 Windows 端 buffer 下标）：

    0   0xDC          帧头 1
    1   0xDC          帧头 2
    2   0x01          厂商号 USB_BRAND_1 = 1 (LE lo)
    3   0x00          厂商号 (LE hi)
    4   1/2/3         设备类别，等于 PID 的低位：0x5201->1, 0x5202->2, 0x5203->3
    5..9  0           保留
    10  index         设备序号（同一 PID 的第 N 个设备，由主机指派）
    11  cmd           命令字（见 Cmd）
    12  ch            通道
    13  mode          模式 / 子命令（LCD 用，见 LcdSub）
    14..  payload
"""

from __future__ import annotations

import struct
from dataclasses import dataclass, field
from enum import IntEnum
from typing import Iterable, Sequence

HEAD = 0xDC
BRAND = 1
HEADER_LEN = 14
DATA_LEN = 64            # 不含 ReportID 的数据长度
MAX_PAYLOAD = DATA_LEN - HEADER_LEN    # 50
REPORT_ID = 0

VID = 0x0416
# PID -> 设备类别
PID_CLASS = {0x5201: 1, 0x5202: 2, 0x5203: 3}
PID_BY_CLASS = {v: k for k, v in PID_CLASS.items()}
CLASS_NAME = {
    1: "风扇/灯效控制器 (FormDevice)",
    2: "SYD 灯效控制器 (FormDeviceSYD)",
    3: "LCD 屏幕 (FormDeviceLCD)",
}


class Cmd(IntEnum):
    STATE = 1        # 设备序号登记（连接/断开时重排 index）
    GET_STATE = 2    # 请求状态（风扇转速/灯效回读），并顺带携带遥测
    AUDIO = 4        # 音频电平 + 遥测，10Hz
    MOTOR = 5        # 保留，原程序未使用
    FAN = 6          # 风扇曲线
    LED = 16         # 灯效
    LCD = 48         # LCD 屏幕（mode 为子命令）


class LcdSub(IntEnum):
    TEMP = 0         # "主板温度,CPU温度,GPU温度,内存温度,CPU风扇,GPU风扇"
    FREQ = 32        # "CPU频率,GPU频率,内存频率,CPU电压,内存电压"
    CLOCK = 64       # "yyyy-MM-dd,HH:mm,HH:mm,"
    CONFIG = 96      # 50 字节：5 组 x 10 字节 显示配置
    NAME = 97        # 背景文件名（ASCII + NUL），随后设备切成 U 盘
    APPLY = 98       # 结束 / 重启到正常模式
    ROT0 = 99
    ROT90 = 100
    ROT180 = 101
    ROT270 = 102
    WHITE = 103
    BLACK = 104
    SCREEN_OFF = 105   # 原程序「自由管理」开：停用其它按钮
    SCREEN_ON = 106


# ---------------------------------------------------------------- 帧构造

def build_frame(cls: int, index: int, cmd: int, ch: int = 0, mode: int = 0,
                payload: bytes | bytearray | None = None) -> bytes:
    """构造 64 字节数据区（不含 ReportID）。"""
    if cls not in PID_BY_CLASS:
        raise ValueError(f"设备类别必须是 1/2/3，收到 {cls!r}")
    body = bytes(payload or b"")
    if len(body) > MAX_PAYLOAD:
        raise ValueError(f"负载最长 {MAX_PAYLOAD} 字节，收到 {len(body)}")
    head = bytearray(HEADER_LEN)
    head[0] = HEAD
    head[1] = HEAD
    struct.pack_into("<H", head, 2, BRAND)
    head[4] = cls
    head[10] = index & 0xFF
    head[11] = cmd & 0xFF
    head[12] = ch & 0xFF
    head[13] = mode & 0xFF
    return bytes(head) + body


def build_report(cls: int, index: int, cmd: int, ch: int = 0, mode: int = 0,
                 payload: bytes | bytearray | None = None) -> bytes:
    """构造真正写往 hidraw 的 65 字节报文（首字节为 ReportID 0）。"""
    data = build_frame(cls, index, cmd, ch, mode, payload)
    return bytes([REPORT_ID]) + data + bytes(DATA_LEN - len(data))


# ---------------------------------------------------------------- 遥测负载

def telemetry_payload(cls: int, *, cpu_temp: int = 0, gpu_temp: int = 0,
                      audio8: int = 0, cpu_usage: int = 0, mem_usage: int = 0,
                      audio12: int = 0) -> bytes:
    """sendDeviceData1 / 2 / 3 在 data==null 时构造的 46 字节默认负载。

    类别 1： [42]=CPU温度 [43]=GPU温度 [44]=音频(0..255)
    类别 2/3：[40]=CPU占用 [41]=内存占用 [42]=CPU温度 [43]=GPU温度 [44..45]=音频(0..4095, LE)
    """
    buf = bytearray(46)
    if cls == 1:
        buf[42] = cpu_temp & 0xFF
        buf[43] = gpu_temp & 0xFF
        buf[44] = audio8 & 0xFF
    else:
        buf[40] = cpu_usage & 0xFF
        buf[41] = mem_usage & 0xFF
        buf[42] = cpu_temp & 0xFF
        buf[43] = gpu_temp & 0xFF
        struct.pack_into("<H", buf, 44, audio12 & 0xFFFF)
    return bytes(buf)


# ---------------------------------------------------------------- 灯效

# 原程序 UserDeviceLED.ButtonMode_Click 的模式号；名称按按钮资源名（拼音）标注，
# 中文含义为推断值，若设备实际表现不同以实测为准。
LED_MODES = {
    1: ("JTDS", "静态单色（默认）"),
    2: ("HXMS", "呼吸模式"),
    3: ("DSXH", "单色循环"),
    4: ("DSBS", "单色闪烁"),
    5: ("XGSS", "星光闪烁"),
    6: ("CHMS", "彩虹模式"),
    7: ("QSPM", "全色频谱"),
    8: ("SXPM", "声显频谱"),
    9: ("ZYJC", "自由键触"),
    10: ("QCZZ", "全彩自动（发全 0 负载）"),
    11: ("CHLS", "彩虹流水"),
    12: ("YYMS", "音乐模式"),
    100: ("WBSR", "外部输入（屏幕取色）"),
}

LED_SPEED_SLOW, LED_SPEED_MID, LED_SPEED_FAST = 4, 2, 1


def led_payload(brightness: int, speed: int, rgb: tuple[int, int, int]) -> bytes:
    r, g, b = (max(0, min(255, int(v))) for v in rgb)
    return bytes([brightness & 0xFF, speed & 0xFF, r, g, b, 0])


# ---------------------------------------------------------------- 风扇

# UserDeviceFAN：模式 1..4 为 4 字节 PWM + 3 字节温度阈值
FAN_CURVES = {
    1: {"name": "静音", "pwm": (0, 0, 65, 100), "temp": (30, 70, 75)},
    2: {"name": "标准", "pwm": (20, 20, 60, 100), "temp": (40, 60, 75)},
    3: {"name": "性能", "pwm": (30, 30, 99, 100), "temp": (25, 70, 75)},
    4: {"name": "全速", "pwm": (97, 98, 99, 100), "temp": (30, 70, 75)},
}
FAN_MODE_CUSTOM = 5


def fan_payload(mode: int, custom: tuple[tuple[int, int, int], tuple[int, int, int]] | None = None) -> bytes:
    """模式 1..4 用预设曲线；模式 5 用自定义（pwm 三点, temp 三点）。

    自定义模式的字节顺序是「0, temp1, temp2, temp3, pwm1, pwm2, pwm3」，
    预设模式是「pwm1..pwm4, temp1..temp3」——两者顺序不同，照抄原程序。
    """
    if mode in FAN_CURVES:
        c = FAN_CURVES[mode]
        return bytes(c["pwm"]) + bytes(c["temp"])
    if mode == FAN_MODE_CUSTOM:
        if custom is None:
            raise ValueError("自定义风扇曲线需要 (pwm三点, temp三点)")
        pwm, temp = custom
        if len(pwm) != 3 or len(temp) != 3:
            raise ValueError("自定义曲线必须是 3 个 PWM 点 + 3 个温度点")
        return bytes([0]) + bytes(temp) + bytes(pwm)
    raise ValueError(f"未知风扇模式 {mode}")


# ---------------------------------------------------------------- LCD

def lcd_text(payload: str) -> bytes:
    """LCD 文本负载（ASCII，逗号分隔，末尾无 NUL）。"""
    return payload.encode("ascii", "replace")


def lcd_name(filename: str) -> bytes:
    """模式 97 的负载：ASCII 文件名 + NUL，最长 46 字节（原程序限制）。"""
    raw = filename.encode("ascii", "replace")
    if len(raw) > MAX_PAYLOAD - 2:
        raise ValueError("文件名过长，设备只接受较短的名字")
    return raw + b"\x00"


@dataclass
class LcdSlot:
    """LCD 显示配置中的一组（原程序 10 字节）。"""
    clock: bool = False        # True = 该组显示时间（isTimerVal）
    layout: int = 3            # myCh：布局 1/2/3 个指标
    hour24: int = 24           # 12 / 24
    m1: int = 0                # 指标组 0=温度 1=风扇 2=频率 3=电压
    m2: int = 0                # 组内序号
    m3: int = 0                # 第二列：组
    m4: int = 0                # 第二列：序号
    m5: int = 0                # 第三列：组
    m6: int = 0                # 第三列：序号
    ext_lo: int = 0            # textBoxN 值低字节
    ext_hi: int = 0            # textBoxN 值高字节

    def pack(self) -> bytes:
        packed = ((self.m1 << 4) | (self.m2 & 0xF),
                  (self.m3 << 4) | (self.m4 & 0xF),
                  (self.m5 << 4) | (self.m6 & 0xF))
        return bytes([1 if self.clock else 0, self.layout & 0xFF, self.hour24 & 0xFF,
                      *packed, self.ext_lo & 0xFF, self.ext_hi & 0xFF])


def lcd_config(slots: Sequence[LcdSlot]) -> bytes:
    """模式 96 的 50 字节负载：5 组 x 10 字节，不足的组用第一组补齐、ext 归零。"""
    if not slots:
        raise ValueError("至少需要一组显示配置")
    out = bytearray()
    for s in slots:
        out += s.pack()
    while len(out) < MAX_PAYLOAD:
        # 原程序：array[num2..num2+5] = array[num2-10..num2-5]（复制上一组），ext 置 0
        prev = bytes(out[-10:])
        out += prev[:6] + b"\x00\x00"
    return bytes(out[:MAX_PAYLOAD])


# ---------------------------------------------------------------- 状态回读

@dataclass
class FanState:
    mode: int = 0
    rpm: int = 0


@dataclass
class LedState:
    mode: int = 0
    brightness: int = 0
    speed: int = 0
    rgb: tuple[int, int, int] = (0, 0, 0)


@dataclass
class DeviceState:
    command: int = 0
    fans: dict[int, FanState] = field(default_factory=dict)
    led: LedState | None = None
    raw: bytes = b""

    def as_text(self) -> str:
        parts = [f"风扇{ch}: {st.mode}档 {st.rpm}RPM" for ch, st in sorted(self.fans.items())]
        if self.led:
            r, g, b = self.led.rgb
            parts.append(f"灯效: 模式{self.led.mode} 亮度{self.led.brightness} 速度{self.led.speed} "
                         f"#{r:02X}{g:02X}{b:02X}")
        return " | ".join(parts) if parts else "（无状态数据）"


def _find_sync(buf: bytes) -> int:
    """定位 0xDC 0xDC 帧头，容忍输入报告是否包含 ReportID。"""
    for i in (0, 1, 2):
        if len(buf) > i + 1 and buf[i] == HEAD and buf[i + 1] == HEAD:
            return i
    return 0


def parse_state(buf: bytes) -> DeviceState | None:
    """解析 GET_STATE 回应。

    绝对偏移（相对帧头）：[12]=2 表示状态回应；
    [15]=风扇1模式 [16..17]=转速(LE) …每 3 字节一组共 4 组；
    [27]=灯效模式 [28]=亮度 [29]=速度 [30..32]=RGB。
    """
    if len(buf) < 27:
        return None
    base = _find_sync(buf)
    if buf[base + 12] != Cmd.GET_STATE:
        return None
    st = DeviceState(command=buf[base + 12], raw=bytes(buf))
    if len(buf) >= base + 27:
        for ch in range(1, 5):
            off = base + 15 + (ch - 1) * 3
            st.fans[ch] = FanState(mode=buf[off], rpm=buf[off + 1] | (buf[off + 2] << 8))
    if len(buf) >= base + 33:
        st.led = LedState(mode=buf[base + 27], brightness=buf[base + 28], speed=buf[base + 29],
                          rgb=(buf[base + 30], buf[base + 31], buf[base + 32]))
    return st


# ---------------------------------------------------------------- 便捷构造

def state_frame(cls: int, index: int) -> bytes:
    """连接握手：cmd=1，携带设备序号指派。"""
    return build_report(cls, index, Cmd.STATE, 0, 0)


def audio_frame(cls: int, index: int, **tele) -> bytes:
    return build_report(cls, index, Cmd.AUDIO, 0, 0, telemetry_payload(cls, **tele))


def get_state_frame(cls: int, index: int, **tele) -> bytes:
    return build_report(cls, index, Cmd.GET_STATE, 0, 0, telemetry_payload(cls, **tele))


def led_frame(cls: int, index: int, channel: int, mode: int, brightness: int, speed: int,
              rgb: tuple[int, int, int]) -> bytes:
    return build_report(cls, index, Cmd.LED, channel, mode, led_payload(brightness, speed, rgb))


def fan_frame(cls: int, index: int, channel: int, mode: int,
              custom: tuple[Sequence[int], Sequence[int]] | None = None) -> bytes:
    return build_report(cls, index, Cmd.FAN, channel, mode, fan_payload(mode, custom))


def lcd_frame(cls: int, index: int, sub: int, payload: bytes = b"") -> bytes:
    return build_report(cls, index, Cmd.LCD, 0, sub, payload)


def hexdump(frame: Iterable[int]) -> str:
    return " ".join(f"{b:02X}" for b in frame)
