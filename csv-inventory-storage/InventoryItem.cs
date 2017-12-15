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
            set => _description = value;
        }

        [CsvSerializable]
        public string InventoryGroup
        {
            get => _inventoryGroup;
            set => _inventoryGroup = value;
        }

        [CsvSerializable]
        public string InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = value;
        }

        [CsvSerializable]
        public string SerialNumber
        {
            get => _serialNumber;
            set => _serialNumber = value;
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
            set => _addedBy = value;
        }

        public string ToCsv()
        {
            return $"{_description};{_inventoryGroup};{_inventoryId};{_serialNumber};{_addedAt:dd.MM.yyyy};{_addedBy}";
        }

        public static InventoryItem FromCsv(string csv)
        {
            // TODO: better serialization

            var parsed = csv.Split(';');

            return new InventoryItem()
                   {
                       Description    = parsed[0],
                       InventoryGroup = parsed[1],
                       InventoryId    = parsed[2],
                       SerialNumber   = parsed[3],
                       AddedAt        = DateTime.Parse(parsed[4]),
                       AddedBy        = parsed[5]
                   };
        }

        public static string Header()
        {
            return "Description;InventoryGroup;InventoryId;SerialNumber;AddedAt;AddedBy";
        }
    }
}
