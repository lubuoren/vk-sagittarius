using System;
using System.Management;

namespace VAIKYRIE;

public class SysProp
{
	public string PhysicalMemory_Capacity { get; set; }

	public string ProcessorName { get; set; }

	public string OperatingSystemBit { get; set; }

	public string Os_Caption { get; set; }

	public string Os_Version { get; set; }

	public string ServicePack { get; set; }

	public string SystemSpecialFolder { get; set; }

	public string Video_Caption { get; set; }

	public string UserName { get; set; }

	public string UserDomainName { get; set; }

	public string CompSys_Workgroup { get; set; }

	public SysProp()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
		ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("select * from Win32_Processor");
		ManagementObjectSearcher searcher3 = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
		ManagementObjectSearcher searcher4 = new ManagementObjectSearcher("select * from Win32_VideoController");
		ManagementObjectSearcher searcher5 = new ManagementObjectSearcher("select * from Win32_ComputerSystem");
		PhysicalMemory_Capacity = string.Format("{0} MB", Convert.ToInt64(GetValue(searcher, "Capacity")) / 1024 / 1024);
		ProcessorName = (string)GetValue(searcher2, "Name");
		OperatingSystemBit = (Environment.Is64BitOperatingSystem ? "64位" : "32位");
		Os_Caption = (string)GetValue(searcher3, "Caption");
		Os_Version = Environment.OSVersion.Version.ToString();
		ServicePack = ((!string.IsNullOrEmpty(Environment.OSVersion.ServicePack)) ? Environment.OSVersion.ServicePack : "无");
		SystemSpecialFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);
		Video_Caption = (string)GetValue(searcher4, "Caption");
		UserName = Environment.UserName;
		UserDomainName = Environment.UserDomainName;
		CompSys_Workgroup = (string)GetValue(searcher5, "Workgroup");
	}

	private object GetValue(ManagementObjectSearcher searcher, string propName)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		ManagementObjectEnumerator enumerator = searcher.Get().GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				ManagementObject val = (ManagementObject)enumerator.Current;
				return ((ManagementBaseObject)val)[propName];
			}
		}
		finally
		{
			((IDisposable)enumerator)?.Dispose();
		}
		throw new NotSupportedException();
	}
}
