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

        [CsvSerializable]
        public string Description {
            get => _description;
            set => _description = _checkSemicolons(value);
        }

        [CsvSerializable]
        public string InventoryGroup
        {
            get => _inventoryGroup;
            set => _inventoryGroup = _checkSemicolons(value);
        }

        [CsvSerializable]
        public string InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = _checkSemicolons(value);
        }

        [CsvSerializable]
        public string SerialNumber
        {
            get => _serialNumber;
            set => _serialNumber = _checkSemicolons(value);
        }

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

        private string _checkSemicolons(string str)
        {
            if (str.Contains(";"))
                throw new System.Exception("Semicolon not allowed in CSV value");
            return str;
        }

        public string ToCsv()
        {
            return CsvSerializer.Serialize(this);
        }
    }
}
