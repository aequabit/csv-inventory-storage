namespace CSVInventoryStorage.Exception
{
    public class CommandRegisterException : System.Exception
    {
        public CommandRegisterException() { }
        public CommandRegisterException(string message) : base(message) { }
        public CommandRegisterException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
