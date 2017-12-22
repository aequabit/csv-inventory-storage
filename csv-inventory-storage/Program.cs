using System;
using CSVInventoryStorage.Cli;
using CSVInventoryStorage.Cli.Commands;

namespace CSVInventoryStorage
{
	internal class Program
    {
	    private const bool Run = true;

	    private static void Main()
        {
            Console.WriteLine("Inventory Storage\n\nType 'help' to show usage information\n");

            Processor.RegisterCommand(new AddItem());
            Processor.RegisterCommand(new RemoveItem());
            Processor.RegisterCommand(new FindItem());
            Processor.RegisterCommand(new EditItem());
            Processor.RegisterCommand(new ListItems());
            Processor.RegisterCommand(new LoadStorage());
            Processor.RegisterCommand(new SaveStorage());

            while (Run) {
                Interface.WriteColor("{darkGray}storage{reset}> ");

                var input = Console.ReadLine()?.Trim();

                string processed;
                try
                {
                    processed = Processor.Process(input);
                }
                catch (Exception ex)
                {
                    Interface.WriteColor("{red}{0}\n\n", ex.Message);
                    continue;
                }

				Interface.WriteColor("{0}\n\n", processed);
            }
        }
    }
}
