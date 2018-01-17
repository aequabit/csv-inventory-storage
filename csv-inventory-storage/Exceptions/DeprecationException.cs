using System;

namespace CSVInventoryStorage.Exceptions
{
	public class DeprecationException : Exception
	{
		public DeprecationException() { }
		public DeprecationException(string message) : base(message) { }
		public DeprecationException(string message, Exception innerException) : base(message, innerException) { }
	}
}
