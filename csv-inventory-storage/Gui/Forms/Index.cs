using System;
using System.Collections.Generic;
using CSVInventoryStorage.UI.Controls;

namespace CSVInventoryStorage.UI.Forms
{
    public class Index : IForm
    {
        public string Name() => "Index";

        public List<IControl> Controls() => new List<IControl>()
        {
            new Button(ConsoleKey.D1, "List items", () => Renderer.Render(new ItemList())),
            new Button(ConsoleKey.D2, "Print stuff", () => {
                Renderer.SetStatus("{red}stuff");
                return false;
            }),
            new Button(ConsoleKey.Backspace, "Exit", () => true)
        };
    }
}
