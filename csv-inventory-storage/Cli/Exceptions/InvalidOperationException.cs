﻿using System;

namespace CSVInventoryStorage.Cli.Exceptions
{
    public class InvalidOperationException : Exception {
        public InvalidOperationException() { }
        public InvalidOperationException(string message): base(message) { }
        public InvalidOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
