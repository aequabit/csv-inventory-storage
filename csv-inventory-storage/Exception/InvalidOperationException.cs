namespace CSVInventoryStorage.Exception
{
    public class InvalidOperationException : System.Exception {
        public InvalidOperationException() { }
        public InvalidOperationException(string message): base(message) { }
        public InvalidOperationException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
