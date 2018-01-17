using System;

namespace CSVInventoryStorage.Gui.Controls
{
	public class Button : IControl
	{
        ConsoleKey Trigger;

        string Text;

        Func<bool> Action;

		public Button(ConsoleKey trigger, string text, Func<bool> action)
		{
            Trigger = trigger;
			Text = text;
            Action = action;
		}

        public ConsoleKey GetTrigger() => Trigger;

		public string GetText() => Text;

		public Func<bool> GetAction() => Action;

        public string Render() => $"{{gray}}{Helper.KeyChar(Trigger)}{{reset}}) {Text}";

        public bool Click() => Action();
	}
}
