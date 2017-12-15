﻿using System;
using CSVInventoryStorage.Command;

namespace CSVInventoryStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            var item = new InventoryItem
            {
                Description    = "des\"cription",
                InventoryGroup = "gr;oup",
                InventoryId    = "id",
                SerialNumber   = "se;ri\"al",
                AddedAt        = DateTime.Now,
                AddedBy        = System.Security.Principal.WindowsIdentity.GetCurrent().Name
            };

            CliProcessor.RegisterCommand("_ser", _args => CsvSerializer.Headers(item) + '\n' + CsvSerializer.Serialize(item));

            CliProcessor.RegisterCommand("_des", _args => {
                var ser = CsvSerializer.Serialize(item);
                var des = CsvSerializer.Deserialize<InventoryItem>(ser);

                return CsvSerializer.Serialize(des);
            });

            Console.WriteLine("Inventory Storage\n\nType 'help' to show usage information\n");

            CliProcessor.RegisterCommand(new CommandAddItem());
            CliProcessor.RegisterCommand(new CommandRemoveItem());
            CliProcessor.RegisterCommand(new CommandEditItem());
            CliProcessor.RegisterCommand(new CommandListItems());

            while (true) {
                Console.Write("storage> ");

                var input = Console.ReadLine()?.Trim();

                string processed;

                try
                {
                    processed = CliProcessor.Process(input);
                }
                catch (System.Exception ex)
                {
                    Console.Write("{0}\n\n", ex.Message);
                    continue;
                }

                Console.Write("{0}\n\n", processed);
            }
        }
    }
}
