using System.Collections.Generic;
using CSVInventoryStorage.Inventory;

namespace CSVInventoryStorage.Cli.Commands
{
	class RunGui : ICommand
	{
		public string CommandName() => "runGui";

		public int ArgCount() => 0;

		public string Usage() => $"{CommandName()}";

		public string Description() => "Switches to GUI mode";

		public string Action(List<string> args)
		{
            Gui.Renderer.Render(new Gui.Forms.Index());

            return "Started GUI";
		}
	}
}
