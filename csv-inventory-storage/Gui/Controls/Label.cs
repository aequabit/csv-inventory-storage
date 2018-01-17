using System;

namespace CSVInventoryStorage.Gui.Controls
{
    public class Label : IControl
    {
        string Text;

        public Label(string text)
        {
            Text = text;
        }

		public Label(string text, params object[] args)
		{
            Text = String.Format(text, args);
		}

        public string GetText() => Text;

        public string Render() => Text;
    }
}
