using System;

namespace CSVInventoryStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inventory Storage\n\nType 'help' to show usage information\n");

            CliProcessor.RegisterCommand(new CommandAddItem());
            CliProcessor.RegisterCommand(new CommandRemoveItem());
            CliProcessor.RegisterCommand(new CommandEditItem());
            CliProcessor.RegisterCommand(new CommandListItems());
            CliProcessor.RegisterCommand(new CommandLoadStorage());
            CliProcessor.RegisterCommand(new CommandSaveStorage());

            while (true) {
                Console.Write("storage> ");

                var input = Console.ReadLine()?.Trim();

                string processed;

                try
                {
                    processed = CliProcessor.Process(input);
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
