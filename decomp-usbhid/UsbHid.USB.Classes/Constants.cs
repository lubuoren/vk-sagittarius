namespace UsbHid.USB.Classes;

public static class Constants
{
	public const int DigcfPresent = 2;

	public const int DigcfDeviceinterface = 16;

	public const int DigcfInterfacedevice = 16;

	public const uint GenericRead = 2147483648u;

	public const uint GenericWrite = 1073741824u;

	public const int FileShareRead = 1;

	public const int FileShareWrite = 2;

	public const int OpenExisting = 3;

	public const int EvRxflag = 2;

	public const int InvalidHandleValue = -1;

	public const int ErrorInvalidHandle = 6;

	public const int FileFlagOverlaped = 1073741824;

	public const int FileFlagOverlapped = 1073741824;

	public const int WaitTimeout = 258;

	public const short WaitObject0 = 0;

	public const short HidPInput = 0;

	public const short HidPOutput = 1;

	public const short HidPFeature = 2;

	internal const int DbtDevicearrival = 32768;

	internal const int DbtDeviceremovecomplete = 32772;

	internal const int DbtDevtypDeviceinterface = 5;

	internal const int DbtDevtypHandle = 6;

	internal const int DbtDevnodesChanged = 7;

	internal const int DeviceNotifyAllInterfaceClasses = 4;

	internal const int DeviceNotifyServiceHandle = 1;

	internal const int DeviceNotifyWindowHandle = 0;

	internal const int WmDevicechange = 537;
}
