using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.UI;

namespace CSVInventoryStorage.UI.Forms
{
    public class ItemView : IForm
    {
        public string FormName() => "Item View";
        public Dictionary<ConsoleKey, Option> Options()
        {
            var final = new Dictionary<ConsoleKey, Option>();

			final.Add(ConsoleKey.Backspace, new Option()
			{
				Name = Item?.InventoryId,
				Action = () => false
			});


			final.Add(ConsoleKey.Backspace, new Option()
            {
                Name = "Go back",
                Action = () => true
            });

            return final;
        }
        public Inventory.Item Item = null;
    }
}