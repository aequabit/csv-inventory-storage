using System;

namespace CSVInventoryStorage
{
    internal class InventoryItem
    {
        private string _description;
        private string _inventoryGroup;
        private string _inventoryId;
        private string _serialNumber;
        private DateTime _addedAt;
        private string _addedBy;

        /// <summary>
        /// Description of the item.
        /// </summary>
        [CsvSerializable]
        public string Description {
            get => _description;
            set => _description = _checkSemicolons(value);
        }

        /// <summary>
        /// Inventory group of the item.
        /// </summary>
        [CsvSerializable]
        public string InventoryGroup
        {
            get => _inventoryGroup;
            set => _inventoryGroup = _checkSemicolons(value);
        }

        /// <summary>
        /// Inventory ID of the item.
        /// </summary>
        [CsvSerializable]
        public string InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = _checkSemicolons(value);
        }

        /// <summary>
        /// Serial number of the item.
        /// </summary>
        [CsvSerializable]
        public string SerialNumber
        {
            get => _serialNumber;
            set => _serialNumber = _checkSemicolons(value);
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
            set => _addedBy = _checkSemicolons(value);
        }

        // TODO: clean up
        private string _checkSemicolons(string str)
        {
            if (str.Contains(";"))
                throw new System.Exception("Semicolon not allowed in CSV value");
            return str;
        }

        /// <summary>
        /// CSV serializes the inventory item.
        /// </summary>
        /// <returns>CSV serialized inventory item.</returns>
        public string ToCsv()
        {
            return CsvSerializer.Serialize(this);
        }
    }
}
