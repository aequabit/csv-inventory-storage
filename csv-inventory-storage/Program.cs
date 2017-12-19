using System;
using CSVInventoryStorage.Cli;
using CSVInventoryStorage.Cli.Commands;

namespace CSVInventoryStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inventory Storage\n\nType 'help' to show usage information\n");

            Processor.RegisterCommand(new AddItem());
            Processor.RegisterCommand(new RemoveItem());
            Processor.RegisterCommand(new EditItem());
            Processor.RegisterCommand(new ListItems());
            Processor.RegisterCommand(new LoadStorage());
            Processor.RegisterCommand(new SaveStorage());

            while (true) {
                Console.Write("storage> ");

                var input = Console.ReadLine()?.Trim();

                string processed;

                try
                {
                    processed = Processor.Process(input);
                }
                catch (Exception ex)
                {
                    Console.Write("{0}\n\n", ex.Message);
                    continue;
                }

                Console.Write("{0}\n\n", processed);
            }
        }
    }
}
