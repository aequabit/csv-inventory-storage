using System;

namespace CSVInventoryStorage
{
    public class InvalidOperandException : Exception
    {
        public InvalidOperandException() { }
        public InvalidOperandException(string message) : base(message) { }
        public InvalidOperandException(string message, Exception innerException) : base(message, innerException) { }
    }
}
