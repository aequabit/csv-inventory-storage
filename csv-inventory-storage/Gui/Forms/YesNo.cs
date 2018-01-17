using System;
using System.Collections.Generic;
using CSVInventoryStorage.Gui.Controls;

namespace CSVInventoryStorage.Gui.Forms
{
    public class YesNo : IForm
    {
        string Title;

        string Prompt;

        Func<bool, bool> Action;

        public YesNo(string title, string prompt, Func<bool, bool> action)
        {
            Title = title;
            Prompt = prompt;
            Action = action;
        }

        public string Name() => Title;

        public List<IControl> Controls() => new List<IControl> {
            new Label(Prompt),
            new Button(ConsoleKey.Y, "Yes", () => Action(true)),
            new Button(ConsoleKey.N, "No", () => Action(false))
        };

        public bool Proxy() => false;

        public bool KeyDown(ConsoleKeyInfo key) => false;
    }
}
