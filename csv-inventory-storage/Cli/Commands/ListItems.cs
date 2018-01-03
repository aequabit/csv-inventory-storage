using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Inventory;

namespace CSVInventoryStorage.Cli.Commands
{
	internal class ListItems : ICommand
    {
        public string CommandName() => "listItems";

        public int ArgCount() => 0;

		public string Usage() => CommandName();
		
        public string Description() => "Lists all items currently in the inventory";

        public string Action(List<string> args)
        {
	        if (Storage.GetInstance().GetItems().Count <= 0)
		        return "{red}No Items found. Please add some items or load any storage.";

			return Storage.GetInstance()
                          .GetItems()
                          .Aggregate("", (current, item) => current + item);
        }
    }
}
