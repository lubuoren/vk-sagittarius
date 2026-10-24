# VK 射手座控制中心 —— 协议逆向记录

对象：`VK射手座控制中心/VAIKYRIE.exe`（.NET Framework 4.x，WinForms，x86）
及 `UsbHid.dll`、`CSCore.dll`、`cpuidsdk.dll`。

反编译工具：`ilspycmd`（.NET 10 SDK 上运行），产物在 `decomp/`、`decomp-usbhid/`。

---

## 1. 设备

三个 HID 设备共用厂商号 `0x0416`（Winbond / Nuvoton 通用 VID），靠 PID 区分功能：

| PID | 类别 | 原程序窗体 | 说明 |
|---|---|---|---|
| `0x5201` | 1 | `FormDevice` | 风扇 + ARGB 灯效控制器 |
| `0x5202` | 2 | `FormDeviceSYD` | SYD 双区灯效控制器 |
| `0x5203` | 3 | `FormDeviceLCD` | LCD 屏幕（带 U 盘模式的 MCU） |

枚举方式：Windows 端 `SetupApi.SetupDiGetClassDevs(HID GUID)` → `HidD_GetAttributes`
→ 只按 VID/PID 过滤（`DeviceDiscovery.FindTargetDevice`），不区分接口与 usage。
Linux 端等价物是 `/sys/class/hidraw/*/device/uevent` 的 `HID_ID`：

```
HID_ID=0003:00000416:00005201
```

实测本机（容器内只映射了 sysfs）就有该节点：

```
/sys/class/hidraw/hidraw0 → HID_NAME=Nuvoton HID Transfer, PID 0x5201
```

## 2. 报文

`UsbHid.USB.Classes.Messaging.CommandMessage`：输出缓冲固定 **65 字节**，
首字节是 ReportID `0`，其后 64 字节数据（按 hidraw 的 `write()` 语义原样写入）。

64 字节数据区分两段：

| 偏移 | 含义 |
|---|---|
| 0 | `0xDC` 帧头 1 |
| 1 | `0xDC` 帧头 2 |
| 2..3 | 厂商号 `1`，小端 |
| 4 | 设备类别 1/2/3（与 PID 一一对应） |
| 5..9 | 保留，全 0 |
| 10 | 设备序号（同类第 N 台，由主机指派） |
| 11 | 命令字 cmd |
| 12 | 通道 ch |
| 13 | 模式 mode（LCD 用作子命令） |
| 14..63 | 负载，≤50 字节 |

连接握手（`UserDevice.DoUsbHidEvent` case 1/16/17）会发一个 14 字节短帧：

```
DC DC 01 00 <类别> 00 00 00 00 00 <序号> 01 00 00
```

即 `cmd=1 (STATE)`，不带负载；断开时对剩余设备重发一遍以重排序号。

## 3. 命令字

| cmd | 常量 | 用途 |
|---|---|---|
| 1 | `USB_PACKED_STATE` | 序号登记 / 状态同步 |
| 2 | `USB_PACKED_GET_STATE` | 请求回读（风扇档位与转速、灯效状态） |
| 4 | `USB_PACKED_AUDIO` | 音频电平 + 遥测，10Hz |
| 5 | `USB_PACKED_MOTOR` | 定义未使用 |
| 6 | `USB_PACKED_FAN` | 风扇曲线 |
| 16 | `USB_PACKED_LED` | 灯效 |
| 48 | `USB_PACKED_LCD` | LCD，mode 为子命令 |

### 3.1 遥测负载（cmd 2 / 4 的 46 字节默认负载）

`UserDevice.sendDeviceData1/2/3` 在 `data == null` 时构造：

| 类别 1 | 类别 2 / 3 |
|---|---|
| `[42]=CPU温度` | `[40]=CPU占用%` |
| `[43]=GPU温度` | `[41]=内存占用%` |
| `[44]=音频 0..255` | `[42]=CPU温度`, `[43]=GPU温度` |
|  | `[44..45]=音频 0..4095`（LE） |

其余字节为 0。

### 3.2 灯效（cmd 16）

`ch`：0=全部通道，1..4=风扇位，5=主板通道
`mode`：1..12 与 100，见下表（名称取自按钮资源名的拼音缩写，中文含义为推断）

| mode | 资源 | 推断含义 |
|---|---|---|
| 1 | JTDS | 静态单色（默认） |
| 2 | HXMS | 呼吸模式 |
| 3 | DSXH | 单色循环 |
| 4 | DSBS | 单色闪烁 |
| 5 | XGSS | 星光闪烁 |
| 6 | CHMS | 彩虹模式 |
| 7 | QSPM | 全色频谱 |
| 8 | SXPM | 声显频谱 |
| 9 | ZYJC | 自由键触 |
| 10 | QCZZ | 全彩自动（负载全 0） |
| 11 | CHLS | 彩虹流水 |
| 12 | YYMS | 音乐模式 |
| 100 | WBSR | 外部输入 |

负载 6 字节：`[亮度 0..100, 速度 1/2/4, R, G, B, 0]`。
速度由滑杆三档映射：<33 → 4（慢），<66 → 2（中），其余 → 1（快）。

SYD 设备（类别 2）另有 `UserDeviceSYD1` 的 19 字节负载与 `cmd = 32/48` 两种
「同显 / 独立显」变体，本移植只实现了灯效页使用的 6 字节形式。

### 3.3 风扇（cmd 6）

预设：`mode` 1..4，负载 7 字节 = `PWM1..PWM4` + `温度1..3`

| mode | PWM 4 点 | 温度 3 点 |
|---|---|---|
| 1 静音 | 0, 0, 65, 100 | 30, 70, 75 |
| 2 标准 | 20, 20, 60, 100 | 40, 60, 75 |
| 3 性能 | 30, 30, 99, 100 | 25, 70, 75 |
| 4 全速 | 97, 98, 99, 100 | 30, 70, 75 |

自定义：`mode=5`，负载为 `0, 温度1, 温度2, 温度3, PWM1, PWM2, PWM3`
——**顺序与预设相反**，这是原程序 `UserDeviceFAN.SendFanData` 的写法，照抄以免翻车。

### 3.4 LCD（cmd 48）

| mode | 名 | 负载 |
|---|---|---|
| 0 | TEMP | ASCII `主板温度,CPU温度,GPU温度,内存温度,CPU风扇,GPU风扇`（无单位，`0.0` 格式） |
| 32 | FREQ | ASCII `CPU频率,GPU频率,内存频率,CPU电压,内存电压` |
| 64 | CLOCK | ASCII `yyyy-MM-dd,HH:mm,HH:mm,`；隐藏时发 ` , , ,` |
| 96 | CONFIG | 50 字节 = 5 组 × 10 字节 |
| 97 | NAME | ASCII 文件名 + `\0`（≤46 字节），触发 U 盘模式 |
| 98 | APPLY | 无负载，结束上传 / 重启回正常模式 |
| 99..102 | ROT0/90/180/270 | 无负载 |
| 103 / 104 | WHITE / BLACK | 无负载 |
| 105 / 106 | SCREEN_OFF / ON | 无负载（原程序「自由管理」开关） |

CONFIG 每组 10 字节（实际只用前 8，后 2 留 0）：

```
[0] 是否显示时间(0/1)  [1] 布局(1/2/3 个指标)  [2] 12/24 小时
[3] (指标组1<<4)|序号   [4] (指标组2<<4)|序号  [5] (指标组3<<4)|序号
[6..7] 附加 u16（原程序 textBoxN 的值，LE）
```

指标组：0=温度，1=风扇，2=频率，3=电压；组内序号见 `FormDeviceLCD` 的
`GetLcdDisplayVal`。不足 5 组时复制上一组前 6 字节，其余补 0。

刷新节奏（`FormDeviceLCD.Timer_event`，1000ms）：TEMP → 330ms → FREQ → 330ms → CLOCK。
风扇/灯效设备另有 100ms 的 cmd 4 心跳与每 10 次一次的 cmd 2 轮询。

## 4. 状态回读

回应帧同样以 `DC DC` 开头，命令回显在相对帧头 +12：

| 相对偏移 | 含义 |
|---|---|
| 12 | `2` = GET_STATE 回应 |
| 15 | 风扇 1 模式 |
| 16..17 | 风扇 1 转速（LE u16） |
| 18..26 | 风扇 2/3/4 同上，每 3 字节一组 |
| 27 | 灯效模式 |
| 28 | 亮度 |
| 29 | 速度 |
| 30..32 | R, G, B |

解析实现会先找 `DC DC` 帧头（允许输入报告带或不带 ReportID 前缀），再按上表取值。

## 5. LCD 换图 / 换视频（关键差异点）

LCD 内容**不走 HID 传数据**。`FormDeviceLCD.buttonBJYY_Click` 的流程：

1. `InitUsbPan()`：记下当前所有盘符；
2. 发 `cmd 48 / mode 97`，负载为文件名；
3. 等 1 秒后最多轮询 30 秒，用 `DriveInfo.GetDrives()` 找出新盘符；
4. `File.Copy(源文件, 新盘符 + 文件名)`；
5. 再等 2 秒，发 `cmd 48 / mode 98` 让设备重启到正常模式。

Linux 移植等价实现（`LcdUploader`）：对比 `/sys/block` 找新块设备 → `udisksctl mount`
（失败则 root 下 `mount -t vfat`）→ 拷贝并 `Flush(true)` 落盘 → 卸载 → 发 98。

## 6. 遥测来源替换

| Windows | Linux |
|---|---|
| WMI `Win32_Processor` / `Win32_VideoController` | `/proc/stat`、`/proc/meminfo`、`/proc/cpuinfo` |
| `cpuidsdk.dll`（CPUID SDK，需 CPU-Z 运行时） | `/sys/class/hwmon`（k10temp / coretemp / zenpower） |
| GPU 温度与转速 | `nvidia-smi --query-gpu=…` 或 amdgpu hwmon |
| CSCore `AudioMeterInformation` | `parec` / `pw-cat` 抓默认 sink 的 monitor 源算峰值 |

`cpuidsdk.dll` 还带试用/过期检查（`You are running a trial version…` / `Evaluation version has expired.`），
移植后完全不需要，也没有授权问题。
