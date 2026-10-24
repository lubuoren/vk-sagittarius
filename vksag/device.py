"""设备门面：把协议帧、传输层与遥测拼成可直接调用的动作。"""

from __future__ import annotations

import time
from dataclasses import dataclass

from . import protocol as P
from .audio import AudioMeter, NullMeter
from .hidraw import Transport, open_devices
from .protocol import DeviceState, LcdSlot, parse_state
from .telemetry import Telemetry


@dataclass
class VkDevice:
    transport: Transport

    # --- 基础
    @property
    def cls(self) -> int:
        return self.transport.cls

    @property
    def index(self) -> int:
        return self.transport.index

    def send(self, report: bytes) -> None:
        self.transport.write(report)

    def register(self) -> None:
        """连接后把本机序号写给设备（cmd=1）。

        原程序在设备连上/断开时重排所有同类设备的 index，设备侧靠这个序号区分
        多个同型号设备；单设备场景写 0 即可。
        """
        self.send(P.state_frame(self.cls, self.index))

    def poll_state(self, timeout: float = 0.5, **tele) -> DeviceState | None:
        self.send(P.get_state_frame(self.cls, self.index, **tele))
        deadline = time.time() + timeout
        while time.time() < deadline:
            data = self.transport.read(0.05)
            if data:
                st = parse_state(data)
                if st:
                    return st
        return None

    # --- 灯效
    def set_led(self, mode: int, rgb=(255, 0, 0), brightness: int = 100,
                speed: int = P.LED_SPEED_FAST, channel: int = 0) -> None:
        self.send(P.led_frame(self.cls, self.index, channel, mode, brightness, speed, rgb))

    def set_fan(self, channel: int, mode: int, custom=None) -> None:
        self.send(P.fan_frame(self.cls, self.index, channel, mode, custom))

    # --- 音频/遥测心跳
    def send_audio(self, tele: Telemetry, meter) -> None:
        self.send(P.audio_frame(
            self.cls, self.index,
            cpu_temp=int(tele.cpu_temp), gpu_temp=int(tele.gpu_temp),
            audio8=meter.audio8, audio12=meter.audio12,
            cpu_usage=tele.cpu_usage, mem_usage=tele.mem_usage))

    # --- LCD
    def lcd_temps(self, tele: Telemetry) -> None:
        payload = ",".join([
            "",                                   # 主板温度（原程序 MainTemp，Linux 略过）
            f"{tele.cpu_temp:.1f}",
            f"{tele.gpu_temp:.1f}",
            "",                                   # 内存温度
            str(tele.cpu_fan_rpm),
            str(tele.gpu_fan_rpm),
        ])
        self.send(P.lcd_frame(self.cls, self.index, P.LcdSub.TEMP, P.lcd_text(payload)))

    def lcd_freqs(self, tele: Telemetry) -> None:
        payload = ",".join([
            f"{tele.cpu_freq_mhz:.0f}",
            f"{tele.gpu_freq_mhz:.0f}",
            f"{tele.mem_freq_mhz:.0f}",
            f"{tele.cpu_voltage:.3f}",
            f"{tele.mem_voltage:.3f}",
        ])
        self.send(P.lcd_frame(self.cls, self.index, P.LcdSub.FREQ, P.lcd_text(payload)))

    def lcd_clock(self, hour24: bool = True, blank: bool = False) -> None:
        if blank:
            payload = " , , ,"
        else:
            now = time.localtime()
            date = time.strftime("%Y-%m-%d", now)
            hm = time.strftime("%H:%M" if hour24 else "%I:%M", now)
            payload = f"{date},{hm},{hm},"
        self.send(P.lcd_frame(self.cls, self.index, P.LcdSub.CLOCK, P.lcd_text(payload)))

    def lcd_config(self, slots: list[LcdSlot]) -> None:
        self.send(P.lcd_frame(self.cls, self.index, P.LcdSub.CONFIG, P.lcd_config(slots)))

    def lcd_simple(self, sub: int) -> None:
        self.send(P.lcd_frame(self.cls, self.index, sub))

    def lcd_name(self, filename: str) -> None:
        self.send(P.lcd_frame(self.cls, self.index, P.LcdSub.NAME, P.lcd_name(filename)))


class DeviceSet:
    """按类别持有设备，默认每类取第一个。"""

    def __init__(self, transports: list[Transport] | None = None):
        self.devices = [VkDevice(t) for t in (transports if transports is not None else open_devices())]
        self.by_class: dict[int, VkDevice] = {}
        for d in self.devices:
            self.by_class.setdefault(d.cls, d)
        self.telemetry = Telemetry()
        self.meter: AudioMeter | NullMeter = NullMeter()

    def __bool__(self) -> bool:
        return bool(self.devices)

    def get(self, cls: int) -> VkDevice | None:
        return self.by_class.get(cls)

    def lcd(self) -> VkDevice | None:
        return self.by_class.get(3)

    def fan_led(self) -> VkDevice | None:
        return self.by_class.get(1) or self.by_class.get(2)

    def register_all(self) -> None:
        for d in self.devices:
            d.register()

    def start_audio(self) -> bool:
        self.meter = AudioMeter()
        if self.meter.start():
            return True
        self.meter = NullMeter()
        return False

    def close(self) -> None:
        self.meter.stop()
        for d in self.devices:
            d.transport.close()
