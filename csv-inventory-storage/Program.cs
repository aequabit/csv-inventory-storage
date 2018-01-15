using System;
using CSVInventoryStorage.Cli;
using CSVInventoryStorage.Cli.Commands;

namespace CSVInventoryStorage
{
    internal class Program
    {
        private static void Main()
        {
            Inventory.Storage.GetInstance().AddItem(new Inventory.Item()
            {
                Description = "test item",
                InventoryGroup = "group1",
                InventoryId = "id1",
                SerialNumber = "#123",
                AddedAt = DateTime.Now,
                AddedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name
            });

			Inventory.Storage.GetInstance().AddItem(new Inventory.Item()
			{
				Description = "test item",
				InventoryGroup = "group2",
				InventoryId = "id2",
				SerialNumber = "#456",
				AddedAt = DateTime.Now,
				AddedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name
			});

            var index = new UI.Forms.Index();
            UI.Renderer.Render(index);

            return;
            Console.WriteLine("Inventory Storage\n\nType 'help' to show usage information\n");

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
