using System;
﻿using System.IO;

namespace CSVInventoryStorage
{
    class CommandSaveStorage : ICommand
    {
        public string CommandName() => "saveStorage";

        public int ArgCount() => 1;

        public string Usage() => CommandName() + " <file>";

        public string Action(object[] args)
        {
            var final = CsvSerializer.Headers(typeof(InventoryItem));
            foreach (var item in Storage.GetInstance().GetItems()) {
              final += '\n' + item.ToCsv();
            }

            var path = (string)args[0];
            File.WriteAllText(path, final);

            return "Saved storage to " + path;
        }
    }
}
