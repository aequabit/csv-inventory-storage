using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVInventoryStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inventory Storage\n\nType 'help' to show usage information\n");

            CliProcessor.RegisterCommand(new CommandAddItem());
            CliProcessor.RegisterCommand(new CommandEditItem());
            CliProcessor.RegisterCommand(new CommandShow());
            CliProcessor.RegisterCommand(new CommandDelItem());

            var lastResult = "0";

            while (true)
            {
                Console.Write("storage> ");

                var input = Console.ReadLine()
                    ?.Trim()
                    .Replace("&l", lastResult);

                string processed;
                try
                {
                    processed = lastResult = CliProcessor.Process(input);
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
