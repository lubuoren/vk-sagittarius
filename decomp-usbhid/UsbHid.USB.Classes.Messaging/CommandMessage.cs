using System;

namespace UsbHid.USB.Classes.Messaging;

public class CommandMessage : IMesage
{
	private byte[] _parameters;

	public byte[] MessageData => GetMessageData();

	public byte[] Parameters
	{
		get
		{
			return _parameters;
		}
		set
		{
			if (value.Length < 1)
			{
				throw new ArgumentOutOfRangeException("value", "Paramater needs to be at least 1 byte long");
			}
			if (value.Length > 64)
			{
				throw new ArgumentOutOfRangeException("value", "Paramater canot be longer than 63 bytes");
			}
			_parameters = value;
		}
	}

	private byte[] GetMessageData()
	{
		byte[] array = new byte[65];
		array[0] = 0;
		if (Parameters != null)
		{
			Array.Copy(Parameters, 0, array, 1, Parameters.Length);
		}
		return array;
	}

	public CommandMessage(byte[] parameters)
	{
		Parameters = parameters;
	}
}
