using System;
using System.Collections.Generic;
using CSVInventoryStorage.Inventory;

namespace CSVInventoryStorage.Cli.Commands
{
    class AddItem : ICommand
    {
        public string CommandName() => "addItem";

        public int ArgCount() => 4;

        public string Usage() => $"{CommandName()} <description> <group> <inventoryId> <serialNumber>";

		public string Description() => "Adds an item to the storage";

		public string Action(List<string> args)
        {
            var inventoryId = args[2];
            if (Storage.GetInstance().GetItemById(inventoryId) != null)
                return $"The ID `{inventoryId}` is already assigned to an item";
            
			var serialNumber = args[3];
            if (Storage.GetInstance().GetItemBySerialNumber(serialNumber) != null) {
                var r = Interface.YesNo($"The serial number `{serialNumber}` is already assigned to an item. Do you want to add it to the inventory anyways?");
                if (!r) return "Aborted due to duplicate serial number";
            }

			Storage.GetInstance().AddItem(new Item
            {
                Description = args[0],
                InventoryGroup = args[1],
                InventoryId = inventoryId,
                SerialNumber = serialNumber,
                AddedAt = DateTime.Now,
                AddedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name
            });

            return "{green}Added item to inventory";
        }
    }
}
