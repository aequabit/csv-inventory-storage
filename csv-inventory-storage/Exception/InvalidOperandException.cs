namespace CSVInventoryStorage.Exception
{
    public class InvalidOperandException : System.Exception
    {
        public InvalidOperandException() { }
        public InvalidOperandException(string message) : base(message) { }
        public InvalidOperandException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
