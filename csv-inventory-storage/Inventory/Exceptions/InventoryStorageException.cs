using System;

namespace CSVInventoryStorage.Inventory.Exceptions
{
    public class InventoryStorageException : Exception
    {
        public InventoryStorageException() { }
        public InventoryStorageException(string message) : base(message) { }
        public InventoryStorageException(string message, Exception innerException) : base(message, innerException) { }
    }
}
