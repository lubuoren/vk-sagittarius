"""音频电平：替代 CSCore 的 AudioMeterInformation（默认播放设备的峰值）。

Windows 端每 100ms 取所有音频会话峰值的最大值，映射到 0..255 / 0..4095。
Linux 端从默认 sink 的 monitor 源抓 16bit 单声道裸流算峰值；
既没有 parec 也没有 pw-cat 时静默返回 0，不影响其它功能。
"""

from __future__ import annotations

import array
import shutil
import subprocess
import threading
import time


class AudioMeter:
    def __init__(self, rate: int = 8000, chunk_ms: int = 20):
        self.rate = rate
        self.chunk = max(1, rate * chunk_ms // 1000)
        self._peak = 0.0
        self._thread: threading.Thread | None = None
        self._stop = threading.Event()
        self._proc: subprocess.Popen | None = None
        self.backend: str | None = None

    # --- public
    def start(self) -> bool:
        if self._thread and self._thread.is_alive():
            return True
        cmd = self._command()
        if cmd is None:
            return False
        self.backend = cmd[0]
        self._thread = threading.Thread(target=self._run, args=(cmd,), daemon=True)
        self._thread.start()
        return True

    def stop(self) -> None:
        self._stop.set()
        if self._proc and self._proc.poll() is None:
            self._proc.terminate()
        if self._thread:
            self._thread.join(timeout=1.0)

    @property
    def level(self) -> float:
        """0.0 .. 1.0 峰值，带轻微衰减避免闪烁。"""
        return self._peak

    @property
    def audio8(self) -> int:
        return max(0, min(255, int(self._peak * 255)))

    @property
    def audio12(self) -> int:
        return max(0, min(4095, int(self._peak * 4095)))

    # --- internals
    def _command(self) -> list[str] | None:
        if shutil.which("parec"):
            return ["parec", "--format=s16le", f"--rate={self.rate}", "--channels=1",
                    "--latency-msec=20", "--device=@DEFAULT_MONITOR@"]
        if shutil.which("pw-cat"):
            return ["pw-cat", "--record", "--raw", "--format=s16", f"--rate={self.rate}",
                    "--channels=1", "--target=@DEFAULT_AUDIO_SINK@", "-"]
        return None

    def _run(self, cmd: list[str]) -> None:
        try:
            self._proc = subprocess.Popen(cmd, stdout=subprocess.PIPE,
                                          stderr=subprocess.DEVNULL, stdin=subprocess.DEVNULL)
        except OSError:
            self.backend = None
            return
        assert self._proc.stdout is not None
        buf = b""
        while not self._stop.is_set():
            data = self._proc.stdout.read(self.chunk * 2)
            if not data:
                break
            buf += data
            samples = array.array("h")
            usable = len(buf) - (len(buf) % 2)
            samples.frombytes(buf[:usable])
            buf = buf[usable:]
            if not samples:
                continue
            peak = max(abs(s) for s in samples) / 32768.0
            # 快升慢降，接近 CSCore 每 100ms 采样的观感
            self._peak = peak if peak > self._peak else self._peak * 0.6
        try:
            if self._proc.poll() is None:
                self._proc.terminate()
        except OSError:
            pass
        self._peak = 0.0


class NullMeter:
    backend = None
    level = 0.0
    audio8 = 0
    audio12 = 0

    def start(self) -> bool:
        return False

    def stop(self) -> None:
        pass
