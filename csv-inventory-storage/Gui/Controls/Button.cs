using System;
namespace CSVInventoryStorage.UI.Controls
{
	public class Button : IControl
	{
        private ConsoleKey Trigger;
		private string Text;
        private Func<bool> Action;

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
