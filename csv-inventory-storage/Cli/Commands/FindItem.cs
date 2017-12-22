using System.Collections.Generic;
using CSVInventoryStorage.Inventory;

namespace CSVInventoryStorage.Cli.Commands
{
	class FindItem : ICommand
	{
		public string CommandName() => "findItem";

		public int ArgCount() => 2;

		public string Usage() => $"{CommandName()} <property> <value>";
		
        public string Description() => "Finds an item by a property and it's value";

		public string Action(List<string> args)
		{
            var property = args[0];
            var value = args[1];

            var item = Storage.GetInstance().GetItem(property, value);

            if (item == null)
                return "No item found";

            return $"Added at: {{darkGray}}{item.AddedAt}{{reset}}\nAdded by: {{darkGray}}{item.AddedBy}{{reset}}\nDescription: {{darkGray}}{item.Description}{{reset}}\n" + $"Group: {{darkGray}}{item.InventoryGroup}{{reset}}\nID: {{darkGray}}{item.InventoryId}{{reset}}\nSerial number: {{darkGray}}{item.SerialNumber}{{reset}}\n\n";
		}
	}
}
