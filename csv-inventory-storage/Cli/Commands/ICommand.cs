using System.Collections.Generic;

namespace CSVInventoryStorage.Cli.Commands
{
    public interface ICommand
    {
        string CommandName();
        int ArgCount();
		string Usage();
		string Description();
        string Action(List<string> args);
    }
}
