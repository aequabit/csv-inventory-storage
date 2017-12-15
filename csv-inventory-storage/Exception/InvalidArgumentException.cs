namespace CSVInventoryStorage.Exception
{
    public class InvalidArgumentException : System.Exception
    {
        public InvalidArgumentException() { }
        public InvalidArgumentException(string message) : base(message) { }
        public InvalidArgumentException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
