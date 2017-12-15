using System.Linq;

namespace CSVInventoryStorage.Command
{
    class CommandRemoveItem : ICommand
    {
        public string CommandName() => "removeItem";

        public int ArgCount() => 1;

        public string Usage() => CommandName() + " <inventoryId>";

        public string Action(object[] args)
        {
            Storage.GetInstance().RemoveItem((string)args.ElementAt(0));

            return "Removed item!";
        }
    }
}
