using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSVInventoryStorage.Inventory;
using CSVInventoryStorage.Serialization;

namespace CSVInventoryStorage.Cli.Commands
{
	class LoadStorage : ICommand
    {
        public string CommandName() => "loadStorage";

        public int ArgCount() => 1;

		public string Usage() => $"{CommandName()} <file>";
		
        public string Description() => "Loads a storage from a CSV file";

        public string Action(List<string> args)
        {
            var path = args[0];
	        if (!path.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
		        path = path + ".csv";

			if (!File.Exists(path))
                throw new FileNotFoundException("File does not exist");

			var items = File.ReadLines(path).ToList();
            if (items.Any() && items.ElementAt(0) == CsvSerializer.Headers(typeof(Item)))
                items.RemoveAt(0);

            var final = items.Select(item => CsvSerializer.Deserialize<Item>(item)).ToList();
	        Storage.GetInstance().SetItems(final);

            return $"Loaded storage from `{path}`";
        }
    }
}
