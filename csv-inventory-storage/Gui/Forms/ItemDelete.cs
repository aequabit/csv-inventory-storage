using System;
using System.Collections.Generic;
using CSVInventoryStorage.UI.Controls;

namespace CSVInventoryStorage.UI.Forms
{
    public class ItemDelete : IForm
    {
        public string Name() => "Confirm deletion";

        public List<IControl> Controls() => new List<IControl>() {
            new Label("Are you sure you want to delete this item?"),
            new Button(ConsoleKey.Y, "Yes", () => {
                // delete
                return true;
            }),
            new Button(ConsoleKey.N, "No", () => true)
        };
    }
}
