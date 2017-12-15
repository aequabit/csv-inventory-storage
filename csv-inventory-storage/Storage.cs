using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Exception;

namespace CSVInventoryStorage
{
    class Storage
    {
        private static Storage _instance;
        private List<InventoryItem> _items = new List<InventoryItem>();

        public static Storage GetInstance()
        {
            return _instance ?? (_instance = new Storage());
        }

        public void AddItem(InventoryItem item) {
          if (_items.Count(x => x.InventoryId == item.InventoryId) > 0)
            throw new InventoryStorageException("Inventory ID already in storage");

          _items.Add(item);
        }

        public void RemoveItem(InventoryItem item) {
          if (_items.Count(x => x.InventoryId == item.InventoryId) > 0)
            throw new InventoryStorageException("Inventory ID not in storage");

          _items.Remove(item);
        }

        public void RemoveItem(string inventoryId) {
          if (_items.All(x => x.InventoryId != inventoryId))
            throw new InventoryStorageException("Inventory ID not in storage");

          _items.RemoveAll(x => x.InventoryId == inventoryId);
        }

        public List<InventoryItem> GetItems() {
          return _items;
        }

        public InventoryItem GetItem(string inventoryId) {
          var matches = _items.Where(x => x.InventoryId == inventoryId);

            var inventoryItems = matches as InventoryItem[] ?? matches.ToArray();
            return !inventoryItems.Any() ? null : inventoryItems.First();
        }
    }
}
