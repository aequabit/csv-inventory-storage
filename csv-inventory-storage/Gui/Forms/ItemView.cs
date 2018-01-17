using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.UI.Controls;

namespace CSVInventoryStorage.UI.Forms
{
    public class ItemView : IForm
    {
        private Inventory.Item Item;

        public ItemView(Inventory.Item item)
        {
            Item = item;
        }

        public string Name() => "Item View";

        public List<IControl> Controls() => new List<IControl>()
        {
			new Label("Description: " + Item.Description),
			new Label("InventoryGroup: " + Item.InventoryGroup),
			new Label("InventoryId: " + Item.InventoryId),
			new Label("SerialNumber: " + Item.SerialNumber),
			new Label("AddedAt: " + Item.AddedAt),
			new Label("AddedBy: " + Item.AddedBy),
            new Button(ConsoleKey.D1, "Delete item", () => Renderer.Render(new ItemDelete())),
			new Button(ConsoleKey.Backspace, "Back", () => true),
        };
    }
}