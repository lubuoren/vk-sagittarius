using System;
using System.Runtime.InteropServices;

namespace UsbHid.DEBUG.Classes;

public class Debugging
{
	internal string ResultOfAPICall(string FunctionName)
	{
		string text = string.Empty;
		try
		{
			text = new string('\0', 129);
			int lastWin32Error = Marshal.GetLastWin32Error();
			long lpSource = 0L;
			int num = DebuggingDeclarations.FormatMessage(4096, ref lpSource, lastWin32Error, 0, text, 128, 0);
			if (num > 2)
			{
				text = text.Remove(num - 2, 2);
			}
			text = "\n" + FunctionName + "\n  Result = " + text.TrimEnd(new char[1]) + "\n";
		}
		catch (Exception)
		{
		}
		return text;
	}
}
