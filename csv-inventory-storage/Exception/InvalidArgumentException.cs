using System;

namespace CSVInventoryStorage
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException() { }
        public InvalidArgumentException(string message) : base(message) { }
        public InvalidArgumentException(string message, Exception innerException) : base(message, innerException) { }
    }
}
