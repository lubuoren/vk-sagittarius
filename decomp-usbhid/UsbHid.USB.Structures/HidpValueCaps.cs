using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures;

[StructLayout(LayoutKind.Explicit)]
public struct HidpValueCaps
{
	[FieldOffset(0)]
	public ushort UsagePage;

	[FieldOffset(2)]
	public byte ReportID;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public bool IsAlias;

	[FieldOffset(4)]
	public ushort BitField;

	[FieldOffset(6)]
	public ushort LinkCollection;

	[FieldOffset(8)]
	public ushort LinkUsage;

	[FieldOffset(10)]
	public ushort LinkUsagePage;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I1)]
	public bool IsRange;

	[FieldOffset(13)]
	[MarshalAs(UnmanagedType.I1)]
	public bool IsStringRange;

	[FieldOffset(14)]
	[MarshalAs(UnmanagedType.I1)]
	public bool IsDesignatorRange;

	[FieldOffset(15)]
	[MarshalAs(UnmanagedType.I1)]
	public bool IsAbsolute;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I1)]
	public bool HasNull;

	[FieldOffset(17)]
	public char Reserved;

	[FieldOffset(18)]
	public ushort BitSize;

	[FieldOffset(20)]
	public ushort ReportCount;

	[FieldOffset(22)]
	public ushort Reserved2a;

	[FieldOffset(24)]
	public ushort Reserved2b;

	[FieldOffset(26)]
	public ushort Reserved2c;

	[FieldOffset(28)]
	public ushort Reserved2d;

	[FieldOffset(30)]
	public ushort Reserved2e;

	[FieldOffset(32)]
	public ushort UnitsExp;

	[FieldOffset(34)]
	public ushort Units;

	[FieldOffset(36)]
	public short LogicalMin;

	[FieldOffset(38)]
	public short LogicalMax;

	[FieldOffset(40)]
	public short PhysicalMin;

	[FieldOffset(42)]
	public short PhysicalMax;

	[FieldOffset(44)]
	public Range Range;

	[FieldOffset(44)]
	public Range NotRange;
}
