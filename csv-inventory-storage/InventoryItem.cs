using System;

namespace CSVInventoryStorage
{
    internal class InventoryItem
    {
        private string _description;
        private string _inventoryGroup;
        private string _inventoryId;
        private string _serialNumber;
        private string _addedBy;

        public string Description {
            get => _description;
            set => _description = Sanitize(value);
        }
        public string InventoryGroup
        {
            get => _inventoryGroup;
            set => _inventoryGroup = Sanitize(value);
        }
        public string InventoryId
        {
            get => _inventoryId;
            set => _inventoryId = Sanitize(value);
        }
        public string SerialNumber
        {
            get => _serialNumber;
            set => _serialNumber = Sanitize(value);
        }
        public DateTime AddedAt { get; set; }

        public string AddedBy
        {
            get => _addedBy;
            set => _addedBy = Sanitize(value);
        }

        public string ToCsv()
        {
            return
                $"{_description};{_inventoryGroup};{_inventoryId};{_serialNumber};{AddedAt:dd.MM.yyyy};{_addedBy}";
        }

        public static InventoryItem FromCsv(string csv)
        {
            var parsed = csv.Split(';');

            return new InventoryItem()
            {
                Description = parsed[0],
                InventoryGroup = parsed[1],
                InventoryId = parsed[2],
                SerialNumber = parsed[3],
                AddedAt = DateTime.Parse(parsed[4]),
                AddedBy = parsed[5]
            };
        }

        public static string Header()
        {
            return "Description;InventoryGroup;InventoryId;SerialNumber;AddedAt;AddedBy";
        }

        public static string Sanitize(string value)
        {
            return value.Replace(";", "").Replace("\"", "");
        }
    }
}
