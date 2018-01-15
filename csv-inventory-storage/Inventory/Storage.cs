using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CSVInventoryStorage.Inventory.Exceptions;

namespace CSVInventoryStorage.Inventory
{
    class Storage
    {
        static string StorageDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		static Storage _instance;
        List<Item> _items = new List<Item>();

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
        public void AddItem(Item item)
        {
            if (_items.Any(x => x.InventoryId == item.InventoryId))
                throw new InventoryStorageException("Inventory ID already in storage");

            _items.Add(item);


        }

        /// <summary>
        /// Removes an item from the storage.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        public void RemoveItem(Item item)
        {
            if (_items.Any(x => x.InventoryId == item.InventoryId))
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
        public void SetItems(List<Item> items)
        {
            _items = items;
        }

        /// <summary>
        /// Gets the item list.
        /// </summary>
        /// <returns>List of items.</returns>
        public List<Item> GetItems()
        {
            return _items;
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
            var matches = _items.Where(x => x.InventoryId == inventoryId);

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
            var matches = _items.Where(x => x.SerialNumber == serialNumber);

            var inventoryItems = matches as Item[] ?? matches.ToArray();
            return !inventoryItems.Any() ? null : inventoryItems.First();
        }
    }
}
