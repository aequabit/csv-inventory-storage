﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVInventoryStorage
{
    class Program
    {
        private static void RegisterCommands()
        {
            CLIProcessor.RegisterCommand("addItem", (object[] args) => {
                Console.Read();
                return "";
            });
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Inventory Storage\n\nType 'help' to show usage information\n");

            string lastResult = "0";

            while (true)
            {
                Console.Write("storage> ");

                string input = Console.ReadLine()
                    .Trim()
                    .Replace("&l", lastResult);

                string processed;
                try
                {
                    processed = lastResult = CLIProcessor.Process(input);
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
