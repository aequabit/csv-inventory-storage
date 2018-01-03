using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Serialization;

namespace CSVInventoryStorage.Inventory
{
    class Item
    {
	    /// <summary>
        /// Description of the item.
        /// </summary>
        [CsvSerializable]
        public string Description { get; set; }

	    /// <summary>
        /// Inventory group of the item.
        /// </summary>
        [CsvSerializable]
        public string InventoryGroup { get; set; }

	    /// <summary>
        /// Inventory ID of the item.
        /// </summary>
        [CsvSerializable]
        public string InventoryId { get; set; }

	    /// <summary>
        /// Serial number of the item.
        /// </summary>
        [CsvSerializable]
        public string SerialNumber { get; set; }

	    /// <summary>
        /// Date the item was added at.
        /// </summary>
        [CsvSerializable]
        public DateTime AddedAt { get; set; }

	    [CsvSerializable]
        public string AddedBy { get; set; }

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
            return $"AddedAt: {{darkGray}}{AddedAt}{{reset}}\nAddedBy: {{darkGray}}{AddedBy}{{reset}}\nDescription: {{darkGray}}{Description}{{reset}}\n" 
				+ $"InventoryGroup: {{darkGray}}{InventoryGroup}{{reset}}\nInventoryId: {{darkGray}}{InventoryId}{{reset}}\nSerialNumber: {{darkGray}}{SerialNumber}{{reset}}\n\n";
        }
    }
}
