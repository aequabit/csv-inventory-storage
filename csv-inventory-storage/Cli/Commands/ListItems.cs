using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Inventory;

namespace CSVInventoryStorage.Cli.Commands
{
    class ListItems : ICommand
    {
        public string CommandName() => "listItems";

        public int ArgCount() => 0;

        public string Usage() => CommandName();

        public string Action(List<string> args)
        {
            return Storage.GetInstance()
                          .GetItems()
                          .Aggregate("", (string current, Item item) => current + item);
        }
    }
}
