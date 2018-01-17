using System;
using System.Collections.Generic;
using CSVInventoryStorage.Gui.Controls;

namespace CSVInventoryStorage.Gui.Forms
{
    public class ItemView : IForm
    {
        Inventory.Item Item;

        public ItemView(Inventory.Item item)
        {
            Item = item;
        }

        public string Name() => "Item View";

        public List<IControl> Controls() => new List<IControl>
        {
            new Label("{0,-16}\t{1}", "Description: ", Item.Description),
            new Label("{0,-16}\t{1}", "InventoryGroup: ", Item.InventoryGroup),
            new Label("{0,-16}\t{1}", "InventoryId: ", Item.InventoryId),
            new Label("{0,-16}\t{1}", "SerialNumber: ", Item.SerialNumber),
            new Label("{0,-16}\t{1}", "AddedBy: ", Item.AddedBy),
            new Label("{0,-16}\t{1}", "AddedAt: ", Item.AddedAt.ToString("dd.MM.yy")),
            new Button(ConsoleKey.D1, "Delete item", () => Renderer.Render(new ItemDelete(Item))),
            new Button(ConsoleKey.Backspace, "Back", () => true)
        };

        public bool Proxy() => false;

        public bool KeyDown(ConsoleKeyInfo key) => false;
    }
}