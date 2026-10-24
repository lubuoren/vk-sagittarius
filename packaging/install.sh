#!/usr/bin/env bash
# VK 射手座控制中心 · Linux 安装脚本
# 用法： ./packaging/install.sh [--prefix /opt/vk-sagittarius] [--no-udev]
set -euo pipefail

PREFIX="/opt/vk-sagittarius"
DO_UDEV=1
HERE="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT="$(cd "$HERE/.." && pwd)"

while [[ $# -gt 0 ]]; do
  case "$1" in
    --prefix) PREFIX="$2"; shift 2 ;;
    --no-udev) DO_UDEV=0; shift ;;
    -h|--help) sed -n '2,4p' "$0"; exit 0 ;;
    *) echo "未知参数：$1"; exit 1 ;;
  esac
done

command -v dotnet >/dev/null || { echo "缺少 dotnet：先装 .NET 10 运行时/SDK"; exit 1; }

echo "→ 发布到 $PREFIX"
dotnet publish "$ROOT/dotnet/src/VkSagittarius.App" \
  -c Release -r linux-x64 --self-contained false -o "$PREFIX"

chmod +x "$PREFIX/VkSagittarius.App"

echo "→ 安装 desktop 入口"
mkdir -p "$HOME/.local/share/applications"
sed "s|/opt/vk-sagittarius|$PREFIX|g" "$HERE/vk-sagittarius.desktop" \
  > "$HOME/.local/share/applications/vk-sagittarius.desktop"

if [[ $DO_UDEV -eq 1 ]]; then
  echo "→ 安装 udev 规则（需要 sudo）"
  sudo install -m644 "$HERE/99-vk-sagittarius.rules" /etc/udev/rules.d/
  sudo udevadm control --reload
  sudo udevadm trigger --subsystem-match=hidraw
fi

echo
echo "完成。运行： $PREFIX/VkSagittarius.App"
echo "自检：     $PREFIX/VkSagittarius.App --self-test"
