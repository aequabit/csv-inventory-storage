using System;

namespace CSVInventoryStorage
{
    class InventoryItem
    {
        private string _Description;
        private string _InventoryGroup;
        private string _InventoryId;
        private string _SerialNumber;
        private DateTime _AddedAt;
        private string _AddedBy;

        public string Description {
            get { return _Description; }
            set { _Description = Sanitize(value); }
        }
        public string InventoryGroup
        {
            get { return _InventoryGroup; }
            set { _InventoryGroup = Sanitize(value); }
        }
        public string InventoryId
        {
            get { return _InventoryId; }
            set { _InventoryId = Sanitize(value); }
        }
        public string SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = Sanitize(value); }
        }
        public DateTime AddedAt
        {
            get { return _AddedAt;  }
            set { _AddedAt = value; }
        }
        public string AddedBy
        {
            get { return _AddedBy; }
            set { _AddedBy = Sanitize(value); }
        }

        public string ToCSV()
        {
            return string.Format("{0};{1};{2};{3};{4};{5}", _Description, _InventoryGroup, _InventoryId, _SerialNumber, _AddedAt.ToString("dd.MM.yyyy"), _AddedBy);
        }

        public static InventoryItem FromCSV(string csv)
        {
            string[] parsed = csv.Split(';');

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
