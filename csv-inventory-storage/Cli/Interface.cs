using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CSVInventoryStorage.Extensions;

namespace CSVInventoryStorage.Cli
{
    public class Interface
    {
        public static Dictionary<string, ConsoleColor> _colors = new Dictionary<string, ConsoleColor>()
        {
            { "black", ConsoleColor.Black },
            { "blue", ConsoleColor.Blue },
            { "cyan", ConsoleColor.Cyan },
            { "darkblue", ConsoleColor.DarkBlue },
            { "darkcyan", ConsoleColor.DarkCyan },
            { "darkgray", ConsoleColor.DarkGray },
            { "darkgreen", ConsoleColor.DarkGreen },
            { "darkmagenta", ConsoleColor.DarkMagenta },
            { "darkred", ConsoleColor.DarkRed },
            { "darkyellow", ConsoleColor.DarkYellow },
            { "gray", ConsoleColor.Gray },
            { "green", ConsoleColor.Green },
            { "magenta", ConsoleColor.Magenta },
            { "red", ConsoleColor.Red },
            { "white", ConsoleColor.White },
            { "yellow", ConsoleColor.Yellow }
        };

        /// <summary>
        /// Console.Write proxy with color tags.
        /// </summary>
        /// <param name="value">Value to write.</param>
        /// <param name="args">Optional formatting arguments.</param>
        public static void WriteColor(string value, params object[] args)
        {
            if (args.Length > 0)
                value = value.FormatNumeric(args);

            var matches = Regex.Matches(value, "{([^{}]*)}");

            var colors = new List<string>();
            for (var i = 0; i < matches.Count; i++)
                colors.Add(matches[i].Groups[1].Value);

            var values = new List<string>();
            var split = Regex.Split(value, "{.*?}").ToList();
            foreach (var s in split)
                if (s != String.Empty) values.Add(s);

            for (var i = 0; i < values.Count(); i++)
            {
                var s = values[i];

                if (i < colors.Count)
                {
                    var color = colors[i].ToLower();
                    if (_colors.ContainsKey(color))
                        Console.ForegroundColor = _colors[color];
                    else if (color == "reset")
                        Console.ResetColor();
                }

                Console.Write(s);
            }
        }

        /// <summary>
        /// Console.WriteLine proxy with color tags.
        /// </summary>
        /// <param name="value">Value to write.</param>
        /// <param name="args">Optional formatting arguments.</param>
        public static void WriteLineColor(string value, params object[] args)
        {
            WriteColor(value + '\n', args);
        }

        /// <summary>
        /// Prints a yes/no question and gets the answer as a boolean.
        /// </summary>
        /// <returns>The user's answer as a boolean.</returns>
        /// <param name="prompt">Question to show to the user.</param>
        public static bool YesNo(string prompt)
        {
            string input = "";
            do
            {
                Console.Write("{0} [y/n] ", prompt);
                input = Console.ReadLine();
            } while (input != "y" && input != "n");

            return input == "y";
        }
    }
}
