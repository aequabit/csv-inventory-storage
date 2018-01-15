using System;
using System.Collections.Generic;
﻿using System.IO;
using System.Linq;
using CSVInventoryStorage.Inventory;
using CSVInventoryStorage.Serialization;

namespace CSVInventoryStorage.Cli.Commands
{
	internal class SaveStorage : ICommand
    {
        public string CommandName() => "saveStorage";

        public int ArgCount() => 1;

		public string Usage() => $"{CommandName()} <file>";

		public string Description() => "Exports the storage to a CSV file";

        public string Action(List<string> args)
        {
			var path = args[0];

            if (File.Exists(path)) {
                var r = Interface.YesNo($"`{path}` already exists, you want to overwrite it?");
                if (!r) return "Aborted due to output file already existing";
            }

            var final = CsvSerializer.Headers(typeof(Item));
	        final = Storage.GetInstance().GetItems().Aggregate(final, (current, item) => current + ('\n' + item.ToCsv()));

			if (!path.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
				path = path + ".csv";
			

	        File.WriteAllText(path, final);

            return $"Saved storage to `{path}`";
        }
    }
}
