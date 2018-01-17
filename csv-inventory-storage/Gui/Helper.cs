using System;
using System.Collections.Generic;

namespace CSVInventoryStorage.UI
{
    public class Helper
    {
        private static Dictionary<ConsoleKey, string> KeyChars = new Dictionary<ConsoleKey, string>()
        {
            { ConsoleKey.Backspace, "←" },
            { ConsoleKey.Enter, "↩" },
            { ConsoleKey.Escape, "Esc" }
        };

        private static Dictionary<int, ConsoleKey> IndexKeyMap = new Dictionary<int, ConsoleKey>()
        {
            { 0, ConsoleKey.D1 },
            { 1, ConsoleKey.D2 },
            { 2, ConsoleKey.D3 },
            { 3, ConsoleKey.D4 },
            { 4, ConsoleKey.D5 },
            { 5, ConsoleKey.D6 },
            { 6, ConsoleKey.D7 },
            { 7, ConsoleKey.D8 },
            { 8, ConsoleKey.D9 },
            { 9, ConsoleKey.D0 }
        };

        private static Dictionary<int, ConsoleKey> DigitKeyMap = new Dictionary<int, ConsoleKey>()
        {
            { 1, ConsoleKey.D1 },
            { 2, ConsoleKey.D2 },
            { 3, ConsoleKey.D3 },
            { 4, ConsoleKey.D4 },
            { 5, ConsoleKey.D5 },
            { 6, ConsoleKey.D6 },
            { 7, ConsoleKey.D7 },
            { 8, ConsoleKey.D8 },
            { 9, ConsoleKey.D9 },
            { 0, ConsoleKey.D0 }
        };

        private static Dictionary<ConsoleKey, int> KeyDigitMap = new Dictionary<ConsoleKey, int>()
        {
            { ConsoleKey.D1, 1 },
            { ConsoleKey.D2, 2 },
            { ConsoleKey.D3, 3 },
            { ConsoleKey.D4, 4 },
            { ConsoleKey.D5, 5 },
            { ConsoleKey.D6, 6 },
            { ConsoleKey.D7, 7 },
            { ConsoleKey.D8, 8 },
            { ConsoleKey.D9, 9 },
            { ConsoleKey.D0, 0 }
        };

        /// <summary>
        /// Gets the string replacement for a ConsoleKey if one exists.
        /// </summary>
        /// <returns>string replacement.</returns>
        /// <param name="key">Key to get the string replacement for.</param>
        public static string KeyChar(ConsoleKey key)
        {
            if (KeyChars.ContainsKey(key))
                return KeyChars[key];

            if (KeyDigitMap.ContainsKey(key))
                return KeyDigitMap[key].ToString();

            return key.ToString();
        }

        /// <summary>
        /// Checks if a digit is a valid number key.
        /// </summary>
        /// <returns><c>true</c>, if digit is a valid number key, <c>false</c> otherwise.</returns>
        /// <param name="digit">Digit to check.</param>
        public static bool ValidKey(int digit) => DigitKeyMap.ContainsKey(digit);

        /// <summary>
        /// Converts a digit to it's corresponding number key.
        /// </summary>
        /// <returns>The digit to convert.</returns>
        /// <param name="digit">Digit to convert.</param>
        public static ConsoleKey DigitToKey(int digit) => DigitKeyMap[digit];

		/// <summary>
		/// Checks if an index is a valid number key.
		/// </summary>
		/// <returns><c>true</c>, if index is a valid number key, <c>false</c> otherwise.</returns>
		/// <param name="index">Index to check.</param>
		public static bool ValidIndex(int index) => IndexKeyMap.ContainsKey(index);

		/// <summary>
		/// Converts an index to it's corresponding number key.
		/// </summary>
		/// <returns>The index to convert.</returns>
		/// <param name="index">Index to convert.</param>
		public static ConsoleKey IndexToKey(int index) => IndexKeyMap[index];

	}
}
