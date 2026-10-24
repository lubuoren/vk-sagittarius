"""Linux 遥测：替代原程序的 WMI + CPUID SDK。

原程序从 WMI (Win32_Processor / Win32_VideoController …) 与 cpuidsdk.dll 取
温度、频率、电压、风扇转速；这里全部改读 /sys (hwmon/cpufreq) 与 /proc，
GPU 走 nvidia-smi 或 amdgpu hwmon。取不到的项返回 0，不抛异常。
"""

from __future__ import annotations

import glob
import os
import shutil
import subprocess
import time
from dataclasses import dataclass

CPU_HWMON_NAMES = ("k10temp", "zenpower", "coretemp", "cpu_thermal", "soc_thermal",
                   "it8xxx2", "nct6", "nct67", "asus", "acpitz")
GPU_HWMON_NAMES = ("amdgpu", "nouveau", "radeon")


def _read(path: str) -> str:
    try:
        with open(path, "r", encoding="utf-8", errors="replace") as fh:
            return fh.read().strip()
    except OSError:
        return ""


def _hwmon_dirs() -> list[tuple[str, str]]:
    out = []
    for d in glob.glob("/sys/class/hwmon/hwmon*"):
        out.append((d, _read(os.path.join(d, "name"))))
    return out


def _first_temp(prefixes: tuple[str, ...]) -> float | None:
    for d, name in _hwmon_dirs():
        if not any(name.startswith(p) or p in name for p in prefixes):
            continue
        # 优先 Tctl / Package / Tdie
        labels = {}
        for lbl in glob.glob(os.path.join(d, "temp*_label")):
            labels[os.path.basename(lbl).replace("_label", "")] = _read(lbl)
        prefer = ("Tctl", "Tdie", "Package id 0", "Composite", "edge")
        for key in prefer:
            for stem, lbl in labels.items():
                if lbl == key:
                    val = _read(os.path.join(d, f"{stem}_input"))
                    if val.isdigit():
                        return int(val) / 1000.0
        for inp in sorted(glob.glob(os.path.join(d, "temp*_input"))):
            val = _read(inp)
            if val.lstrip("-").isdigit():
                return int(val) / 1000.0
    return None


def _first_fan_rpm() -> int:
    for d, _name in _hwmon_dirs():
        for inp in sorted(glob.glob(os.path.join(d, "fan*_input"))):
            val = _read(inp)
            if val.isdigit() and int(val) > 0:
                return int(val)
    return 0


def _cpu_freq_mhz() -> float:
    for cpu in sorted(glob.glob("/sys/devices/system/cpu/cpu[0-9]*/cpufreq/scaling_cur_freq")):
        val = _read(cpu)
        if val.isdigit() and int(val) > 0:
            return int(val) / 1000.0
    # 退路：/proc/cpuinfo
    mhz = []
    for line in _read("/proc/cpuinfo").splitlines():
        if line.lower().startswith("cpu mhz"):
            try:
                mhz.append(float(line.split(":", 1)[1]))
            except ValueError:
                pass
    return sum(mhz) / len(mhz) if mhz else 0.0


def _meminfo() -> tuple[int, int]:
    total = avail = 0
    for line in _read("/proc/meminfo").splitlines():
        if line.startswith("MemTotal:"):
            total = int(line.split()[1]) * 1024
        elif line.startswith("MemAvailable:"):
            avail = int(line.split()[1]) * 1024
    return total, avail


def _cpu_times() -> tuple[int, int]:
    parts = _read("/proc/stat").splitlines()[0].split()
    nums = [int(x) for x in parts[1:]]
    idle = nums[3] + (nums[4] if len(nums) > 4 else 0)
    return sum(nums), idle


@dataclass
class GpuInfo:
    temp: float = 0.0
    usage: int = 0
    freq: float = 0.0
    fan: int = 0


def _nvidia() -> GpuInfo | None:
    if not shutil.which("nvidia-smi"):
        return None
    try:
        res = subprocess.run(
            ["nvidia-smi",
             "--query-gpu=temperature.gpu,utilization.gpu,clocks.current.graphics,fan.speed",
             "--format=csv,noheader,nounits"],
            capture_output=True, text=True, timeout=2.0, check=False)
    except (OSError, subprocess.SubprocessError):
        return None
    line = (res.stdout or "").strip().splitlines()
    if not line:
        return None
    fields = [f.strip() for f in line[0].split(",")]
    info = GpuInfo()
    try:
        info.temp = float(fields[0])
        info.usage = int(float(fields[1]))
        info.freq = float(fields[2])
        info.fan = int(float(fields[3])) if fields[3] not in ("[N/A]", "") else 0
    except (ValueError, IndexError):
        return None
    return info


def _amdgpu() -> GpuInfo | None:
    for d, name in _hwmon_dirs():
        if name not in GPU_HWMON_NAMES:
            continue
        info = GpuInfo()
        val = _read(os.path.join(d, "temp1_input"))
        if val.lstrip("-").isdigit():
            info.temp = int(val) / 1000.0
        val = _read(os.path.join(d, "fan1_input"))
        if val.isdigit():
            info.fan = int(val)
        sclk = _read(os.path.join(d, "device", "pp_dpm_sclk"))
        for line in sclk.splitlines():
            if line.rstrip().endswith("*"):
                try:
                    info.freq = float(line.split(":")[1].split("Mhz")[0])
                except (IndexError, ValueError):
                    pass
        return info
    return None


class Telemetry:
    """周期性采样，返回 LCD 文本与 HID 遥测字节所需的整数/字符串。"""

    def __init__(self) -> None:
        self._last_total = 0
        self._last_idle = 0
        self._last_ts = 0.0
        self._cpu_usage = 0
        self._gpu = GpuInfo()

    def sample(self) -> "Telemetry":
        total, idle = _cpu_times()
        if self._last_total:
            dt = total - self._last_total
            di = idle - self._last_idle
            if dt > 0:
                self._cpu_usage = max(0, min(100, int(round(100.0 * (dt - di) / dt))))
        self._last_total, self._last_idle = total, idle

        gpu = _nvidia() or _amdgpu()
        self._gpu = gpu or GpuInfo()
        self._last_ts = time.time()
        return self

    # --- 数值
    @property
    def cpu_temp(self) -> float:
        return _first_temp(CPU_HWMON_NAMES) or 0.0

    @property
    def gpu_temp(self) -> float:
        return self._gpu.temp

    @property
    def cpu_usage(self) -> int:
        return self._cpu_usage

    @property
    def mem_usage(self) -> int:
        total, avail = _meminfo()
        if not total:
            return 0
        return max(0, min(100, int(round(100.0 * (total - avail) / total))))

    @property
    def cpu_freq_mhz(self) -> float:
        return _cpu_freq_mhz()

    @property
    def gpu_freq_mhz(self) -> float:
        return self._gpu.freq

    @property
    def mem_freq_mhz(self) -> float:
        # 内存频率：/sys/devices/system/edac 或 dmidecode 需 root，退化到 0
        for base in glob.glob("/sys/devices/system/edac/mc/mc*"):
            val = _read(os.path.join(base, "dimm*/ddr_speed_mts"))
            if not val:
                for f in glob.glob(os.path.join(base, "dimm*", "ddr_speed_mts")):
                    val = _read(f)
                    break
            if val.isdigit():
                return float(val)
        return 0.0

    @property
    def cpu_fan_rpm(self) -> int:
        return _first_fan_rpm()

    @property
    def gpu_fan_rpm(self) -> int:
        return self._gpu.fan

    @property
    def cpu_voltage(self) -> float:
        for d, name in _hwmon_dirs():
            if name.startswith(("k10temp", "zenpower", "coretemp")):
                val = _read(os.path.join(d, "in0_input"))
                if val.isdigit():
                    return int(val) / 1000.0
        return 0.0

    @property
    def mem_voltage(self) -> float:
        return 0.0
