using System;
using System.Collections.Generic;
using CSVInventoryStorage.Gui.Controls;

namespace CSVInventoryStorage.Gui.Forms
{
    public class ItemDelete : IForm
    {
        Inventory.Item Item;

        public ItemDelete(Inventory.Item item)
        {
            Item = item;
        }

        public string Name() => "Confirm Deletion";

        public List<IControl> Controls() => new List<IControl> {
            new Label("Are you sure you want to delete this item?"),
            new Button(ConsoleKey.Y, "Yes", () => {
                Inventory.Storage.GetInstance().RemoveItem(Item.InventoryId);
                return true;
            }),
            new Button(ConsoleKey.N, "No", () => true)
        };

        public bool Proxy() => false;

        public bool KeyDown(ConsoleKeyInfo key) => false;
    }
}
