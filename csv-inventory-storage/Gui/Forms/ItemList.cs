using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.UI.Controls;

namespace CSVInventoryStorage.UI.Forms
{
    public class ItemList : IForm
    {
        public string Name() => "Item List";

        public List<IControl> Controls()
        {
            var final = new List<IControl>() {
                new Label(String.Format("   {{gray}}{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}", "Description", "InventoryGroup", "InventoryId", "SerialNumber", "AddedBy", "AddedAt"))
            };

            var items = Inventory.Storage.GetInstance().GetItems();

            for (var i = 0; i < items.Count(); i++)
            {
                var item = items[i];

                var format = String.Format("{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}",
                                           item.Description,
                                           item.InventoryGroup,
                                           item.InventoryId,
                                           item.SerialNumber,
                                           item.AddedBy,
                                           item.AddedAt.ToString("dd.MM.yy"));

                if (Helper.ValidKey(i))
                    final.Add(
                        new Button(
                            Helper.DigitToKey(i),
                            format,
                            () => Renderer.Render(new ItemView(item))));
            }

            final.Add(new Button(ConsoleKey.Backspace, "Back", () => true));

            return final;
        }
    }
}