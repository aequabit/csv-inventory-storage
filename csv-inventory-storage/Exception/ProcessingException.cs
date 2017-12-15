namespace CSVInventoryStorage.Exception
{
    public class ProcessingException : System.Exception {
        public ProcessingException() { }
        public ProcessingException(string message): base(message) { }
        public ProcessingException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
