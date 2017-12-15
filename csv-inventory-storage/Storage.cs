using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Exception;

namespace CSVInventoryStorage
{
    class Storage
    {
        private static Storage _instance = null;
        private List<InventoryItem> _items = new List<InventoryItem>();

        public static Storage GetInstance() {
          if (_instance == null)
            _instance = new Storage();
          return _instance;
        }

        public void AddItem(InventoryItem item) {
          if (_items.Where(x => x.InventoryId == item.InventoryId).Count() > 0)
            throw new InventoryStorageException("Inventory ID already in storage");

          _items.Add(item);
        }

        public void RemoveItem(InventoryItem item) {
          if (_items.Where(x => x.InventoryId == item.InventoryId).Count() > 0)
            throw new InventoryStorageException("Inventory ID not in storage");

          _items.Remove(item);
        }

        public void RemoveItem(string inventoryId) {
          if (_items.Where(x => x.InventoryId == inventoryId).Count() == 0)
            throw new InventoryStorageException("Inventory ID not in storage");

          _items.RemoveAll(x => x.InventoryId == inventoryId);
        }

        public List<InventoryItem> GetItems() {
          return _items;
        }

        public InventoryItem GetItem(string inventoryId) {
          var matches = _items.Where(x => x.InventoryId == inventoryId);

          if (matches.Count() == 0)
            return null;

          return matches.First();
        }
    }
}
