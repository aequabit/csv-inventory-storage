using System;
using System.Collections.Generic;

namespace CSVInventoryStorage.Cli.Commands
{
	class EditItem : ICommand
    {
        public string CommandName() => "editItem";

        public int ArgCount() => -1;

		public string Usage() => $"{CommandName()} <...>";
		
        public string Description() => "Edits an item";

        public string Action(List<string> args)
        {
            // TODO: implement edit command
            throw new NotImplementedException("editItem is not implemented yet.");
        }
    }
}
