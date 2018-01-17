using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Inventory.Exceptions;

namespace CSVInventoryStorage.Inventory
{
    class Storage
    {
        /// <summary>
        /// Current intance.
        /// </summary>
        static Storage Instance;

        /// <summary>
        /// List of items in the storage.
        /// </summary>
        List<Item> Items = new List<Item>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSVInventoryStorage.Inventory.Storage"/> class.
        /// </summary>
        Storage() => Cache.Initialize();

        /// <summary>
        /// Gets the current instance.
        /// </summary>
        /// <returns>Storage instance.</returns>
        public static Storage GetInstance()
        {
            if (Instance == null)
                Instance = new Storage();

            Instance.SetItems(Cache.RetrieveItems(), false);

            return Instance;
        }

        /// <summary>
        /// Adds an item to the storage.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void AddItem(Item item)
        {
            if (Items.Any(x => x.InventoryId == item.InventoryId))
                throw new InventoryStorageException("Inventory ID already in storage");

            Cache.CacheItem(item);

            Items.Add(item);
        }

        /// <summary>
        /// Removes an item from the storage.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        public void RemoveItem(Item item) => RemoveItem(item.InventoryId);

        /// <summary>
        /// Removes an item from the storage.
        /// </summary>
        /// <param name="inventoryId">Inventory ID of the item.</param>
        public void RemoveItem(string inventoryId)
        {
            if (Items.All(x => x.InventoryId != inventoryId))
                throw new InventoryStorageException("Inventory ID not in storage");

            var match = Items.Find(item => item.InventoryId == inventoryId);
            if (match != null)
                Cache.RemoveItem(match);

			Items.RemoveAll(x => x.InventoryId == inventoryId);
        }

        /// <summary>
        /// Sets the item list.
        /// </summary>
        /// <param name="items">Item list.</param>
        /// <param name="updateCache">If true, the cache will be overwritten with the new items.</param>
        public void SetItems(List<Item> items, bool overwriteCache = true)
        {
            Items = items;

            if (overwriteCache)
				Cache.SetItems(items);
		}

        /// <summary>
        /// Gets the item list.
        /// </summary>
        /// <returns>List of items.</returns>
        public List<Item> GetItems()
        {
            return Items;
        }

        /// <summary>
        /// Gets an item by a property.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="propertyName">Property to get the item by.</param>
        /// <param name="value">Value to get the item by.</param>
        public Item GetItem(string propertyName, string value)
        {
            var props = typeof(Item).GetProperties();

            var matches = props.Where(prop => prop.Name == propertyName);
            if (!matches.Any())
                return null;

            var property = matches.First();

            var items = GetItems().Where(item => property.GetValue(item, null).ToString() == value);
            if (!items.Any())
                return null;

            return items.First();
        }

        /// <summary>
        /// Gets an item by it's inventory ID.
        /// </summary>
        /// <returns>Inventory ID of the item.</returns>
        /// <param name="inventoryId">Inventory ID.</param>
        public Item GetItemById(string inventoryId)
        {
            var matches = Items.Where(x => x.InventoryId == inventoryId);

            var inventoryItems = matches as Item[] ?? matches.ToArray();
            return !inventoryItems.Any() ? null : inventoryItems.First();
        }

        /// <summary>
        /// Gets an item by it's serial number.
        /// </summary>
        /// <returns>Serial number of the item.</returns>
        /// <param name="serialNumber">Serial number.</param>
        public Item GetItemBySerialNumber(string serialNumber)
        {
            var matches = Items.Where(x => x.SerialNumber == serialNumber);

            var inventoryItems = matches as Item[] ?? matches.ToArray();
            return !inventoryItems.Any() ? null : inventoryItems.First();
        }
    }
}
