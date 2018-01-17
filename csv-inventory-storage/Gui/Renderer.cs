using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Cli;
using CSVInventoryStorage.Gui.Controls;
using CSVInventoryStorage.Gui.Forms;

namespace CSVInventoryStorage.Gui
{
    public static class Renderer
    {
        /// <summary>
        /// Status message.
        /// </summary>
        static string Status = String.Empty;

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
        static bool Draw(IForm form)
        {
            Console.Clear();

            Interface.WriteLineColor($"{{gray}}{form.Name()}");
            Interface.WriteLineColor($"-");

            foreach (var control in form.Controls())
                Interface.WriteLineColor(control.Render());

            Interface.WriteColor($"{{gray}}>{{reset}} {Status}");

            SetStatus("");

            var info = Console.ReadKey();

            if (form.Proxy())
                if (form.KeyDown(info))
                    return false;

            // TODO: cleanup
            var buttons = form.Controls().Where(control => control.GetType() == typeof(Button));
            var keyButtons = buttons
                .Where(button => ((Button)button).GetTrigger() == info.Key);

            if (keyButtons.Any())
                return ((Button)keyButtons.First()).Click();

            return false;
        }

        /// <summary>
        /// Sets a status message.
        /// </summary>
        /// <param name="message">Status message.</param>
        public static void SetStatus(string message) => Status = $"{{gray}}{message}{{reset}}";

		/// <summary>
		/// Sets a success message.
		/// </summary>
		/// <param name="message">Success message.</param>
		public static void SetSuccess(string message) => Status = $"{{green}}{message}{{reset}}";

		/// <summary>
		/// Sets a warning message.
		/// </summary>
		/// <param name="message">Warning message.</param>
		public static void SetWarning(string message) => Status = $"{{yellow}}{message}{{reset}}";

		/// <summary>
		/// Sets an error message.
		/// </summary>
		/// <param name="message">Error message.</param>
		public static void SetError(string message) => Status = $"{{red}}{message}{{reset}}";
    }
}
