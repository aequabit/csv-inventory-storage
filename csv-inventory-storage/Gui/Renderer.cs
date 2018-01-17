using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Cli;
using CSVInventoryStorage.UI.Controls;
using CSVInventoryStorage.UI.Forms;

namespace CSVInventoryStorage.UI
{
    public class Renderer
    {
        private static string Status = String.Empty;

		/// <summary>
		/// Renders the specified form.
		/// </summary>
		/// <returns><c>true</c>, if the render loop should break, <c>false</c> otherwise.</returns>
		/// <param name="form">Form to render.</param>
		public static bool Render(IForm form)
        {
            while (true)
                if (Draw(form))
                    return false;
        }

		/// <summary>
		/// Draws the specified form.
		/// </summary>
		/// <returns><c>true</c>, if the render loop should break, <c>false</c> otherwise.</returns>
		/// <param name="form">Form to draw.</param>
		private static bool Draw(IForm form)
        {
            Console.Clear();

            Interface.WriteLineColor($"{{gray}}{form.Name()}");
            Interface.WriteLineColor($"-");

            foreach (var control in form.Controls())
                Interface.WriteLineColor(control.Render());

            Interface.WriteColor($"{{gray}}>{{reset}} {Status}");

            SetStatus("");

            var info = Console.ReadKey();

            // TODO: cleanup
            var buttons = form.Controls().Where(control => control.GetType() == typeof(Button));
            var keyButtons = buttons
                .Where(button => ((Button)button).GetTrigger() == info.Key);

            if (keyButtons.Count() > 0)
                return ((Button)keyButtons.First()).Click();

            return false;
        }

        /// <summary>
        /// Sets the status message.
        /// </summary>
        /// <param name="message">Message to set.</param>
        public static void SetStatus(string message) => Status = message;
    }
}
