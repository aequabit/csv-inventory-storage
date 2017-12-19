using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Exception;

namespace CSVInventoryStorage
{
    class Storage
    {
        private static Storage _instance;
        private List<InventoryItem> _items = new List<InventoryItem>();

        /// <summary>
        /// Gets the current instance.
        /// </summary>
        /// <returns>Storage instance.</returns>
        public static Storage GetInstance()
        {
            return _instance ?? (_instance = new Storage());
        }

        /// <summary>
        /// Adds an item to the storage.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void AddItem(InventoryItem item)
        {
            if (_items.Count(x => x.InventoryId == item.InventoryId) > 0)
                throw new InventoryStorageException("Inventory ID already in storage");

            _items.Add(item);
        }

        /// <summary>
        /// Removes an item from the storage.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        public void RemoveItem(InventoryItem item)
        {
            if (_items.Count(x => x.InventoryId == item.InventoryId) > 0)
                throw new InventoryStorageException("Inventory ID not in storage");

            _items.Remove(item);
        }

        /// <summary>
        /// Removes an item from the storage.
        /// </summary>
        /// <param name="inventoryId">Inventory ID of the item.</param>
        public void RemoveItem(string inventoryId)
        {
            if (_items.All(x => x.InventoryId != inventoryId))
                throw new InventoryStorageException("Inventory ID not in storage");

            _items.RemoveAll(x => x.InventoryId == inventoryId);
        }

        /// <summary>
        /// Sets the item list.
        /// </summary>
        /// <param name="items">Item list.</param>
        public void SetItems(List<InventoryItem> items)
        {
            _items = items;
        }

        /// <summary>
        /// Gets the item list.
        /// </summary>
        /// <returns>List of items.</returns>
        public List<InventoryItem> GetItems()
        {
            return _items;
        }

        /// <summary>
        /// Gets an item by it's inventory ID.
        /// </summary>
        /// <returns>Inventory ID of the item.</returns>
        /// <param name="inventoryId">Inventory identifier.</param>
        public InventoryItem GetItem(string inventoryId)
        {
            var matches = _items.Where(x => x.InventoryId == inventoryId);

            var inventoryItems = matches as InventoryItem[] ?? matches.ToArray();
            return !inventoryItems.Any() ? null : inventoryItems.First();
        }
    }
}
