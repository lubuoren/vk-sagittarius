using System;
using System.Threading;
using System.Windows.Forms;

namespace VAIKYRIE;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Thread.Sleep(200);
		bool createdNew;
		Mutex mutex = new Mutex(initiallyOwned: true, Application.ProductName, out createdNew);
		if (createdNew)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run((Form)(object)new Form1());
			mutex.ReleaseMutex();
		}
		else
		{
			MessageBox.Show((IWin32Window)null, "VAIKYRIE 已经打开，不能重复打开！", Application.ProductName, (MessageBoxButtons)0, (MessageBoxIcon)48);
			Application.Exit();
		}
	}
}
