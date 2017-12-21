using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Serialization;

namespace CSVInventoryStorage.Inventory
{
    class Item
    {
        string _description;
        string _inventoryGroup;
        string _inventoryId;
        string _serialNumber;
        DateTime _addedAt;
        string _addedBy;

        /// <summary>
        /// Description of the item.
        /// </summary>
        [CsvSerializable]
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        /// <summary>
        /// Inventory group of the item.
        /// </summary>
        [CsvSerializable]
        public string InventoryGroup
        {
            get => _inventoryGroup;
            set => _inventoryGroup = value;
        }

        /// <summary>
        /// Inventory ID of the item.
        /// </summary>
        [CsvSerializable]
        public string InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = value;
        }

        /// <summary>
        /// Serial number of the item.
        /// </summary>
        [CsvSerializable]
        public string SerialNumber
        {
            get => _serialNumber;
            set => _serialNumber = value;
        }

        /// <summary>
        /// Date the item was added at.
        /// </summary>
        [CsvSerializable]
        public DateTime AddedAt
        {
            get => _addedAt;
            set => _addedAt = value;
        }

        [CsvSerializable]
        public string AddedBy
        {
            get => _addedBy;
            set => _addedBy = value;
        }

        /// <summary>
        /// CSV serializes the inventory item.
        /// </summary>
        /// <returns>CSV serialized inventory item.</returns>
        public string ToCsv()
        {
            return CsvSerializer.Serialize(this);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:CSVInventoryStorage.Inventory.Item"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:CSVInventoryStorage.Inventory.Item"/>.</returns>
        public override string ToString()
        {
            return $"AddedAt: {{darkGray}}{AddedAt}{{reset}}\nAddedBy: {{darkGray}}{AddedBy}{{reset}}\nDescription: {{darkGray}}{Description}{{reset}}\n" + $"InventoryGroup: {{darkGray}}{InventoryGroup}{{reset}}\nInventoryId: {{darkGray}}{InventoryId}{{reset}}\nSerialNumber: {{darkGray}}{SerialNumber}{{reset}}\n\n";
        }
    }
}
