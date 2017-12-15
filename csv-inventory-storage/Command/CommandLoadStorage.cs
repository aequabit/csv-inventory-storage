using System;
using System.Collections.Generic;
﻿using System.IO;
using System.Linq;

namespace CSVInventoryStorage
{
    class CommandLoadStorage : ICommand
    {
        public string CommandName() => "loadStorage";

        public int ArgCount() => 1;

        public string Usage() => CommandName() + " <file>";

        public string Action(object[] args)
        {
            var path = (string)args[0];
            if (!File.Exists(path))
                throw new Exception("File does not exist");

            var csv = File.ReadAllText(path);
            var items = csv.Split('\n').ToList();

            if (items.Count() > 0 && items.ElementAt(0) == CsvSerializer.Headers(typeof(InventoryItem)))
              items.RemoveAt(0);

            var final = new List<InventoryItem>();
            foreach (var item in items)
              final.Add(CsvSerializer.Deserialize<InventoryItem>(item));

            Storage.GetInstance().SetItems(final);

            return "Loaded storage from " + path;
        }
    }
}
