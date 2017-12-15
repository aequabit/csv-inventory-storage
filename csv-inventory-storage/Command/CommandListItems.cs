namespace CSVInventoryStorage
{
    class CommandListItems : ICommand
    {
        public string CommandName() => "listItems";

        public int ArgCount() => 0;

        public string Usage() => CommandName();

        public string Action(object[] args)
        {
            var final = "";
            foreach (var item in Storage.GetInstance().GetItems())
              final += $"Added at: {item.AddedAt}\nAdded by: {item.AddedBy}\nDescription: {item.Description}\n" +
              $"Group: {item.InventoryGroup}\nID: {item.InventoryId}\nSerial number: {item.SerialNumber}\n\n";
            return final;
        }
    }
}
