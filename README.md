# VK 射手座控制中心 · Linux 移植

把 Windows 上的 `VAIKYRIE.exe`（.NET Framework 4.x / WinForms）逆向后，用 **.NET 10 + Avalonia**
重写成能在 Linux 上跑的等价控制中心：LCD 屏遥测、ARGB 灯效、四路风扇曲线、背景图/视频上传。

逆向出的完整线协议见 [`PROTOCOL.md`](PROTOCOL.md)。

![灯效页](screenshots/ui-led.png)

## 目录

```
dotnet/
  src/VkSagittarius.Core/    协议 + hidraw 传输 + 遥测 + 音频电平 + U 盘上传 + 心跳引擎
  src/VkSagittarius.App/     Avalonia 界面（灯效 / 风扇 / LCD 三页 + 日志）
  tests/VkSagittarius.Tests/ 14 项单测（帧布局、负载字节序、状态解析、心跳调度）
  tools/Screenshot/          headless 渲染截图（无显示器环境验证 UI）
  dist/                      dotnet publish 产物（framework-dependent, linux-x64）
vksag/                       最早的 Python 参考实现（协议对照用，不参与构建）
decomp/  decomp-usbhid/      ilspycmd 反编译出的 C# 源码
app/                         原始 Windows 程序解包
screenshots/                 界面截图
packaging/                   udev 规则、desktop 入口、安装脚本
```

## 构建与运行

需要 .NET 10 SDK（`dotnet --version` ≥ 10.0）。仓库：<https://github.com/lubuoren/vk-sagittarius>（private）

```bash
cd dotnet
dotnet build VkSagittarius.slnx
dotnet test tests/VkSagittarius.Tests        # 14 passed
dotnet run --project src/VkSagittarius.App   # 有设备则直连，无设备自动进演示模式
```

发布：

```bash
dotnet publish src/VkSagittarius.App -c Release -r linux-x64 --self-contained false -o dist
./dist/VkSagittarius.App            # 图形界面
./dist/VkSagittarius.App --self-test  # 无界面自检：枚举设备、发一轮帧、打印遥测
```

命令行参数：

| 参数 | 行为 |
|---|---|
| 无 | 打开 HID 设备；一个都打不开时退化为演示模式（写内存假设备） |
| `--demo` | 强制演示模式，界面完整可操作，帧不会真发 |
| `--self-test` | 不进图形界面，打印设备枚举、遥测、一轮 LED/FAN/LCD 发送结果 |
| `--headless [--seconds N]` | 常驻心跳：不开窗口，只跑 10Hz 音频帧 + 1Hz 状态回读 + 1Hz LCD 三段文本；N 秒后自退（验证用） |

## 为什么显示"演示模式"

右上角横幅会直接写原因，鼠标悬停看完整信息。三种情况：

- **权限不足**：`/dev/hidraw*` 在 Arch 上默认是 `root:root 0660`，普通用户打不开，
  程序只能退化成演示模式。装 udev 规则（`makepkg -si` 会带上）后执行
  `sudo udevadm control --reload && sudo udevadm trigger --subsystem-match=hidraw`，
  再点界面上的「重新扫描」即可，不必重启程序。
- **没有设备**：`--self-test` 会打印枚举结果，确认 VID `0x0416`、PID `0x5201-0x5203`
  是否出现。
- **手动 `--demo`**：强制演示模式，横幅会写"手动指定"。

早期版本会吞掉 open 的异常并静默退化成演示模式，现在每台设备的失败原因都会
记进日志与横幅。

## 权限

`/dev/hidraw*` 默认只有 root 可写。装 udev 规则即可免 sudo：

```bash
sudo install -m644 packaging/99-vk-sagittarius.rules /usr/lib/udev/rules.d/
sudo udevadm control --reload-rules
sudo udevadm trigger --action=add --subsystem-match=hidraw   # 注意是 add，change 不会补 ACL
```

规则内容（VID `0x0416`，PID `0x5201/0x5202/0x5203`）：`uaccess` 给图形会话用户 ACL，
`GROUP="input"` 兜底（无活动 seat、远程会话时靠组权限）。

```
SUBSYSTEM=="hidraw", ATTRS{idVendor}=="0416", ATTRS{idProduct}=="5201", TAG+="uaccess", GROUP="input", MODE="0660"
SUBSYSTEM=="hidraw", ATTRS{idVendor}=="0416", ATTRS{idProduct}=="5202", TAG+="uaccess", GROUP="input", MODE="0660"
SUBSYSTEM=="hidraw", ATTRS{idVendor}=="0416", ATTRS{idProduct}=="5203", TAG+="uaccess", GROUP="input", MODE="0660"
```

上传背景图还需要挂载能力：装了 `udisks2` 时普通用户即可；否则以 root 运行，
程序会退回 `mount -t vfat`。

## 复刻范围（对照原程序逐项）

| 原程序 | 状态 | 说明 |
|---|---|---|
| 灯效：12 种模式 + 外部输入、6 通道（全部/1-4/主板） | ✅ | 模式 10「全彩自动」按 `SendLEDData1` 发 mode=1 + 全零负载，与原程序一致 |
| 灯效：首次回读同步模式/亮度/速度/RGB | ✅ | 对齐 `UserDeviceLED.DoInitMyData` |
| 风扇：4 通道 × 4 预设 + 自定义三点曲线 | ✅ | 自定义字节序 `0,temp1..3,pwm1..3` 照抄 |
| 风扇：每通道独立记忆（`myALLMode`）+ 回读同步 | ✅ | 切通道会带回该路上次的模式 |
| 风扇：四路实时转速 | ✅ | 取自 `GetState` 回应的 `[15+3(ch-1) .. ]` |
| 心跳：100ms 音频/遥测 + 1s 状态回读 | ✅ | 合并为 20ms 调度循环，不互相阻塞 |
| 系统信息页 + 温度曲线 | ✅ | WMI/CPUID → `/proc`、hwmon、nvidia-smi；曲线 60 点 1Hz |
| 开机自启 | ✅ | 原程序写注册表 Run，这里写 `~/.config/autostart`（`--headless`） |
| 托盘图标 + 右键退出 | ✅ | Avalonia `TrayIcon`，Linux 无 StatusNotifier 时自动跳过 |
| LCD：三段文本 / 配置 96 / 旋转 / 白黑屏 / 上传背景 | ✅（协议）+ ⚠️（UI 简化） | 显示项选择的完整表单（5 组 × 指标 × 阈值）尚未做成可视化编辑器 |
| SYD 设备（类别 2，19 字节负载、cmd 32/48 双区） | ❌ 未实现 | 只保留了通用 6 字节灯效路径；需要该型号设备才能验证 |
| 启动动画（FormStart） | ❌ 未实现 | 原程序是欢迎页，移植版直接进主界面 |

明确没做的部分不再含糊：SYD 型号、LCD 配置可视化编辑、启动页。

## 与 Windows 版的行为差异

- **遥测来源**：WMI + `cpuidsdk.dll` 换成 `/proc`、`/sys/class/hwmon`、`nvidia-smi`；
  CPUID SDK 本身带试用期检查，移植后不再需要。
- **主板温度 / 内存温度**：Linux 侧普遍没有对应传感器，LCD 上这两项留空（设备显示 0）。
- **音频电平**：需要 `parec`（pulseaudio-utils）或 `pw-cat`（pipewire）；都没有时
  声显频谱类灯效会停在 0，其余功能不受影响。
- **心跳调度**：原程序三个 `System.Timers.Timer` 并发；移植版用一个 20ms 调度循环，
  LCD 三段文本排队发送（0/330/660ms），避免阻塞 10Hz 的音频帧。
- **多台同型号设备**：仍按类别分配序号（`cmd=1`）并写入帧头第 10 字节，
  但界面一次只操作每类的第一台。

## 已验证 / 未验证

已验证（在本机真实执行）：

- `dotnet build` 全解决方案 0 警告 0 错误；15 项单测全过。
- `--self-test` 枚举到宿主透出的 `Nuvoton HID Transfer`（`0x0416:0x5201`），
  读到了真实 CPU 温度 / 频率 / 风扇转速。
- Avalonia headless + Skia 渲染四页界面（灯效/风扇/LCD/系统）并落盘截图（`screenshots/`），中文正常。

未验证（缺硬件或缺少权限）：

- 与真实设备的写入路径：容器内 `/dev/hidraw0` 未映射（`errno=2`），
  因此只验证了帧字节与调度，没有验证设备实际响应。
- LCD 换图依赖设备切成 U 盘后由宿主枚举块设备，容器内无法完整走通。

真机首次验证建议顺序：`--self-test` → 灯效页改颜色 → 风扇页切预设 →
LCD 页旋转 90° → 最后再试上传背景。

## 打包（Arch / AUR 形式）

`packaging/PKGBUILD` + `packaging/.SRCINFO` 可直接 `makepkg`：

```bash
cd packaging
makepkg -si            # 需要 dotnet-sdk（构建期会联网做一次 NuGet 还原）
```

要点：

- 包内路径 `/opt/vk-sagittarius/`，命令入口 `/usr/bin/vk-sagittarius`；
- 自包含发布，装完不依赖 `dotnet-runtime`，包体积约 100 MB；
- 顺手装上 udev 规则、desktop 入口、hicolor 图标与 `systemd --user` 的
  `vk-sagittarius.service`（`--headless` 常驻心跳，不开窗口）；
- `optdepends`：`udisks2`（LCD 换背景挂 U 盘）、`pulseaudio`/`pipewire`（音频电平灯效）；
- 源码包 `vk-sagittarius-1.0.0.tar.gz` 的 sha256 已写入 PKGBUILD 与 .SRCINFO，
  改版本时两处一起改。

已有构建产物（不想自己编译时）：`vk-sagittarius-1.0.0-linux-x64.tar.gz`，
解压后直接跑里面的 `VkSagittarius.App`。

## 配置持久化

`~/.config/vk-sagittarius/config.json` 保存：界面缩放、灯效（模式/通道/亮度/速度/颜色）、
风扇（每通道曲线模式 + 自定义三点 PWM/温度）。点「应用灯效」「应用风扇曲线」写入，
重启后自动恢复。原程序的 `config.d` 只有常量定义、从未写盘，所以曲线点在 Windows 版
重启就丢；这是移植时补的。

## 界面缩放

Hyprland 开 `xwayland:force_zero_scaling` 时，显示器的 fractional scale（如 1.875）
不会传给 X11 程序，Avalonia 按 1.0 画，于是 4K 屏上界面偏小、还会被合成器放大发虚。

优先级：`--scale 1.875` / 环境变量 `VKSAG_SCALE` → `~/.config/vk-sagittarius/config.json`
→ Xft.dpi/96 → 1.0。界面「系统」页里也能直接选（1.0/1.25/1.5/1.75/1.875/2.0），
选中即生效并写回配置；缩放走 `LayoutTransform`，矢量重绘，不是位图拉伸。
