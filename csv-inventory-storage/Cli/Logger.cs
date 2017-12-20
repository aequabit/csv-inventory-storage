using System;
using System.Runtime.InteropServices;

namespace CSVInventoryStorage.Cli
{
	public class Logger
	{
		public static ConsoleColor COLOR_DEFAULT = ConsoleColor.Gray;

		private static object _lock = new object();

		public static void Log(string message, ConsoleColor color = ConsoleColor.Gray)
		{
			lock (_lock)
			{
				Console.ForegroundColor = color;
				Console.Write(message);
				Console.ResetColor();
			}
		}
	}
}
