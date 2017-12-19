using System;

namespace CSVInventoryStorage.Cli.Exceptions
{
    public class ProcessingException : Exception {
        public ProcessingException() { }
        public ProcessingException(string message): base(message) { }
        public ProcessingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
