namespace CSVInventoryStorage.Exception
{
    public class InventoryStorageException : System.Exception
    {
        public InventoryStorageException() { }
        public InventoryStorageException(string message) : base(message) { }
        public InventoryStorageException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
