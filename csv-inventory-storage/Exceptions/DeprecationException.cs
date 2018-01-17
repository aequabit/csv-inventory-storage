using System;

namespace CSVInventoryStorage.Gui.Exceptions
{
	public class DeprecationException : Exception
	{
		public CommandRegisterException() { }
		public CommandRegisterException(string message) : base(message) { }
		public CommandRegisterException(string message, Exception innerException) : base(message, innerException) { }
	}
}
