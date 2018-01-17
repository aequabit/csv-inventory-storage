using System;

namespace CSVInventoryStorage.UI.Controls
{
    public class Label : IControl
    {
        private string Text;

        public Label(string text)
        {
            Text = text;
        }

        public string GetText() => Text;

        public string Render() => Text;
    }
}
