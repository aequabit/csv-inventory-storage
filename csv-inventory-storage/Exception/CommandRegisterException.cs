using System;

namespace CSVInventoryStorage
{
    public class CommandRegisterException : Exception
    {
        public CommandRegisterException() { }
        public CommandRegisterException(string message) : base(message) { }
        public CommandRegisterException(string message, Exception innerException) : base(message, innerException) { }
    }
}
