using System.Linq;
using CSVInventoryStorage.Inventory;

namespace CSVInventoryStorage.Cli.Commands
{
    class ListItems : ICommand
    {
        public string CommandName() => "listItems";

        public int ArgCount() => 0;

        public string Usage() => CommandName();

        public string Action(object[] args)
        {
            return Storage.GetInstance().GetItems().Aggregate("", (current, item) => current + ($"Added at: {item.AddedAt}\nAdded by: {item.AddedBy}\nDescription: {item.Description}\n" + $"Group: {item.InventoryGroup}\nID: {item.InventoryId}\nSerial number: {item.SerialNumber}\n\n"));
        }
    }
}
