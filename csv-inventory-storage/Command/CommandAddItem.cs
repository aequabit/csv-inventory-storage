using System;
using System.Linq;

namespace CSVInventoryStorage
{
    class CommandAddItem : ICommand
    {
        readonly string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        public string CommandName() => "addItem";

        public int ArgCount() => 1;

        public string Usage() => ""
            + "Bitte geben Sie eine Bezeichnung ein: \n"
            + CommandName() + " <name>";

        public string Action(object[] args)
        {
            var description = (string)args.ElementAt(0);

            Console.WriteLine("Bitte geben Sie für das Inventarobjekt \"" + description + "\" die Inventargruppe ein: ");
            var inventoryGroup = Console.ReadLine().Trim();

            Console.WriteLine("Bitte geben Sie für das Inventarobjekt \"" + description + "\" die Inventarnummer ein: ");
            var inventoryNumber = Console.ReadLine().Trim();

            Console.WriteLine("Bitte geben Sie für das Inventarobjekt \"" + description + "\" die Seriennummer ein: ");
            var serialNumber = Console.ReadLine().Trim();

            var item = new InventoryItem
            {
                AddedAt = new DateTime(),
                AddedBy = userName,
                Description = (string)args.ElementAt(0),
                InventoryGroup = inventoryGroup,
                InventoryId = inventoryNumber,
                SerialNumber = serialNumber
            };

            return "neues inventoryitem von " + userName + " :: " + (string) args.ElementAt(0);
        }
    }
}
