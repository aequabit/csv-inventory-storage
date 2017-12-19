using System;

namespace CSVInventoryStorage.Cli.Exceptions
{
    public class CommandRegisterException : Exception
    {
        public CommandRegisterException() { }
        public CommandRegisterException(string message) : base(message) { }
        public CommandRegisterException(string message, Exception innerException) : base(message, innerException) { }
    }
}
