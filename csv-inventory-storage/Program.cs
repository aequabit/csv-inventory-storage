using System;
using CSVInventoryStorage.Cli;
using CSVInventoryStorage.Cli.Commands;
using CSVInventoryStorage.Extensions;
using CSVInventoryStorage.Gui;
using CSVInventoryStorage.Gui.Forms;

namespace CSVInventoryStorage
{
    class Program
    {
        static void RunGui()
        {
            try
            {
                Renderer.Render(new Index());
            }
            catch (Exception ex)
            {
                Renderer.SetError(ex.Message);
            }
        }

        static void RunCli()
        {
            Console.WriteLine("Inventory Storage\n\nType 'help' to show usage information\n");

            Processor.RegisterCommand(new RunGui());
            Processor.RegisterCommand(new AddItem());
            Processor.RegisterCommand(new RemoveItem());
            Processor.RegisterCommand(new FindItem());
            Processor.RegisterCommand(new EditItem());
            Processor.RegisterCommand(new ListItems());
            Processor.RegisterCommand(new LoadStorage());
            Processor.RegisterCommand(new SaveStorage());

            while (true)
            {
                Interface.WriteColor("{darkGray}storage{reset}> ");

                var input = Console.ReadLine()?.Trim();

                string processed;
                try
                {
                    processed = Processor.Process(input.Trim());
                }
                catch (Exception ex)
                {
                    Interface.WriteColor("{red}{0}\n\n", ex.Message);
                    continue;
                }

                Interface.WriteColor("{0}\n\n", processed);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length == 0 || (args.Length > 0 && args[0] != "-cli"))
                RunGui();
            RunCli();
        }
    }
}
