namespace CSVInventoryStorage.Cli.Commands
{
    class EditItem : ICommand
    {
        public string CommandName() => "editItem";

        public int ArgCount() => 1;

        public string Usage() => CommandName() + " <...>";

        public string Action(object[] args)
        {
            // TODO: show all // show 1 // show 5 // ...
            return "edit item placeholder";
        }
    }
}
