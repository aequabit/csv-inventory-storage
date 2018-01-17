using System;
using System.Collections.Generic;
using CSVInventoryStorage.Gui.Controls;

namespace CSVInventoryStorage.Gui.Forms
{
    public class TextInput : IForm
    {
        string Title;

        string Prompt;

        Action<string> Action;

        string Text;

        public TextInput(string title, string prompt, Action<string> action, string defaultValue = "")
        {
            Title = title;
            Prompt = prompt;
            Action = action;
            Text = defaultValue;
        }

        public string Name() => Title;

        public List<IControl> Controls() => new List<IControl>
        {
            new Label(Prompt),
            new Label("{gray}>{reset} " + Text + '\n'),
            new Button(ConsoleKey.Enter, "Submit", () => true),
            new Button(ConsoleKey.Escape, "Abort", () => true)
        };

        public bool Proxy() => true;

        public bool KeyDown(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.Backspace:
                    if (Text.Length > 0)
                        Text = Text.Substring(0, Text.Length - 1);
                    return true;
                case ConsoleKey.Enter:
                    Action(Text);
                    return false;
                case ConsoleKey.Escape:
                    return false;
            }

            Text = Text + key.KeyChar;

            return true;
        }
    }
}
