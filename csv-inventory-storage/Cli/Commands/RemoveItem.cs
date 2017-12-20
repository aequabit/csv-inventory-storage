﻿using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Inventory;

namespace CSVInventoryStorage.Cli.Commands
{
    class RemoveItem : ICommand
    {
        public string CommandName() => "removeItem";

        public int ArgCount() => 1;

        public string Usage() => $"{CommandName()} <inventoryId>";

        public string Action(List<string> args)
        {
            Storage.GetInstance().RemoveItem((string)args.ElementAt(0));

            return "Removed item!";
        }
    }
}
