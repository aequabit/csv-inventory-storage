using System;

namespace CSVInventoryStorage.Cli.Exceptions
{
    public class InvalidOperandException : Exception
    {
        public InvalidOperandException() { }
        public InvalidOperandException(string message) : base(message) { }
        public InvalidOperandException(string message, Exception innerException) : base(message, innerException) { }
    }
}
