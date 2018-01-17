using System;
using System.Collections.Generic;
using CSVInventoryStorage.Exceptions;

namespace CSVInventoryStorage.Gui.Controls
{
	public class TextBox : IControl
	{
        static Dictionary<string, TextBox> Instances = new Dictionary<string, TextBox>();

		string Text = "";

        public TextBox()
        {
            throw new DeprecationException("The TextBox control is deprecated. Use the TextInput form instead.");
        }

        /// <summary>
        /// Gets a TextBox instance by it's identifier.
        /// </summary>
        /// <returns>The textbox.</returns>
        /// <param name="id">Identifier.</param>
		public static TextBox GetById(string id)
        {
            if (Instances.ContainsKey(id))
                return Instances[id];

            return null;
        }

        /// <summary>
        /// Registers the specified TextBox.
        /// </summary>
        /// <param name="id">Unique identifier for the textbox.</param>
        /// <param name="textBox">Textbox to register.</param>
        public static void Register(string id, TextBox textBox)
        {
            if (!Instances.ContainsKey(id))
                Instances.Add(id, textBox);
		}

		/// <summary>
		/// Unregisters the specified TextBox.
		/// </summary>
		/// <param name="id">Identifier of the textbox.</param>
		public static void Unregister(string id)
        {
			if (Instances.ContainsKey(id))
				Instances.Remove(id);
		}

		/// <summary>
		/// Gets the textbox with the specified identifier and creates it if it doesn't exist.
		/// </summary>
		/// <returns>The textbox.</returns>
		/// <param name="id">The identifier to search for.</param>
		public static TextBox GetOrRegister(string id)
        {
            if (Instances.ContainsKey(id))
                return Instances[id];

            var textBox = new TextBox();
            Instances.Add(id, textBox);

            return textBox;
        }

		public string GetText() => Text;

		public void Invoke(char input) => Text += input;

		public void Invoke(string input) => Text += input;

        public void Backspace() => Text = Text.Substring(0, Text.Length - 1);

		public string Render() => "> " + Text;
	}
}
