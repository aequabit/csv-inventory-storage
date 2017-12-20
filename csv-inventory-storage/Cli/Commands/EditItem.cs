using System.Collections.Generic;

namespace CSVInventoryStorage.Cli.Commands
{
    class EditItem : ICommand
    {
        public string CommandName() => "editItem";

        public int ArgCount() => 1;

        public string Usage() => $"{CommandName()} <...>";

        public string Action(List<string> args)
        {
            return "";
        }
    }
}
