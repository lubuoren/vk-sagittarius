using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace VAIKYRIE;

public class NetworkAdapter
{
	private NetworkInterface myNetwork;

	private long dlSpeed;

	private long ulSpeed;

	private long dlValue;

	private long ulValue;

	private long dlValueOld;

	private long ulValueOld;

	internal string name;

	internal PerformanceCounter dlCounter;

	internal PerformanceCounter ulCounter;

	public string Name => name;

	public long DownloadSpeed => dlSpeed;

	public long UploadSpeed => ulSpeed;

	public double DownloadSpeedKbps => (double)dlSpeed / 1024.0;

	public double UploadSpeedKbps => (double)ulSpeed / 1024.0;

	internal NetworkAdapter(string name)
	{
		this.name = name;
		myNetwork = null;
		dlSpeed = (ulSpeed = 0L);
	}

	public void NetworkAdapterNew(NetworkInterface network)
	{
		myNetwork = network;
	}

	internal void init()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (name == null && myNetwork == null)
		{
			return;
		}
		if (name == null)
		{
			try
			{
				dlValueOld = myNetwork.GetIPStatistics().BytesReceived;
				ulValueOld = myNetwork.GetIPStatistics().BytesSent;
				return;
			}
			catch
			{
				MessageBox.Show("myNetwork error!");
				return;
			}
		}
		CounterSample val = dlCounter.NextSample();
		dlValueOld = ((CounterSample)(ref val)).RawValue;
		val = ulCounter.NextSample();
		ulValueOld = ((CounterSample)(ref val)).RawValue;
	}

	internal void refresh()
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (name == null && myNetwork == null)
		{
			return;
		}
		if (name == null)
		{
			try
			{
				dlValue = myNetwork.GetIPStatistics().BytesReceived;
				ulValue = myNetwork.GetIPStatistics().BytesSent;
			}
			catch
			{
			}
			dlSpeed = dlValue - dlValueOld;
			ulSpeed = ulValue - ulValueOld;
			dlValueOld = dlValue;
			ulValueOld = ulValue;
		}
		else
		{
			CounterSample val = dlCounter.NextSample();
			dlValue = ((CounterSample)(ref val)).RawValue;
			val = ulCounter.NextSample();
			ulValue = ((CounterSample)(ref val)).RawValue;
			dlSpeed = dlValue - dlValueOld;
			ulSpeed = ulValue - ulValueOld;
			dlValueOld = dlValue;
			ulValueOld = ulValue;
		}
	}

	public override string ToString()
	{
		return name;
	}
}
