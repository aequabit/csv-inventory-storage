using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Cli;
using CSVInventoryStorage.UI.Forms;

namespace CSVInventoryStorage.UI
{
    public class Renderer
    {
        private static Dictionary<ConsoleKey, string> KeyChars = new Dictionary<ConsoleKey, string>()
        {
            {ConsoleKey.Backspace, "←"},
            {ConsoleKey.Enter, "↩"}
        };

        private static string Status = String.Empty;

        public static bool Render(IForm form)
        {
            while (true)
                if (Draw(form))
                    return false;
        }

		/// <summary>
		/// Gets the string replacement for a ConsoleKey if one exists.
		/// </summary>
		/// <returns>string replacement.</returns>
		/// <param name="key">Key to get the string replacement for.</param>
		private static string KeyChar(ConsoleKey key)
        {
            if (KeyChars.ContainsKey(key))
                return KeyChars[key];

            return key.ToString();
        }

        public static bool Draw(IForm form)
        {
            Console.Clear();

            Interface.WriteLineColor($"{{gray}}{form.FormName()}");
            Interface.WriteLineColor($"-");

            foreach (var pair in form.Options())
                Interface.WriteLineColor($"{KeyChar(pair.Key).Replace("D", "")}) {pair.Value.Name}");

            Interface.WriteColor($"{{gray}}>{{reset}} {Status}");

            SetStatus("");

            var info = Console.ReadKey();
            if (form.Options().ContainsKey(info.Key))
                return form.Options()[info.Key].Action();

            return true;
        }

        /// <summary>
        /// Sets the status message.
        /// </summary>
        /// <param name="message">Message to set.</param>
        public static void SetStatus(string message) => Status = message;
    }
}
