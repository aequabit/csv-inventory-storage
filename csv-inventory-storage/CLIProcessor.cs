using System;
using System.Collections.Generic;
using System.Linq;

namespace CSVInventoryStorage
{
    /// <summary>
    /// Commandline processor.
    /// </summary>
    public class CLIProcessor
    {
        private static Dictionary<string, Func<object[], string>> m_dCommands = new Dictionary<string, Func<object[], string>>() {
            { "help", (object[] args) => { return "Usage: <command|operator|function> [<operands...>]\nExample input: + 5 10 15\n\nAvailable commands:\n  help - Displays available commands\n  exit - Exits SimpleCalc\n\nAvailable operators:\n  + - Adds two or more operands\n  - - Subtracts two or more operands\n  * - Multiplies two or more numbers\n  / - Divides two or more numbers\n\nAvailable functions:\n  sin - Calculates the sinus of a number\n  cos - Calculates the cosinus of a number\n  tan - Calculates the tangent of a number\n  deg2rad - Converts an angle from degrees to radians\n\nAvailable placeholders:\n  &l - Reference to the last result"; } },
            { "exit", (object[] args) => { Environment.Exit(0); return "Exiting..."; } }
        };

        /// <summary>
        /// Registers a command handler.
        /// </summary>
        /// <param name="commandName">Name of the command to register.</param>
        /// <param name="handler">Command handler delegate to register.</param>
        public static void RegisterCommand(string commandName, Func<object[], string> handler)
        {
            if (m_dCommands.ContainsKey(commandName))
                throw new CommandRegisterException("Command already registered");

            m_dCommands.Add(commandName, handler);
        }

        /// <summary>
        /// Unregisters a command handler.
        /// </summary>
        /// <param name="commandName">Name of the command to unregister.</param>
        public static void UnregisterCommand(string commandName)
        {
            if (!m_dCommands.ContainsKey(commandName))
                throw new CommandRegisterException("Command not registered");

            m_dCommands.Remove(commandName);
        }

        /// <summary>
        /// Processes an input string.
        /// </summary>
        /// <param name="input">Input to process.</param>
        /// <returns>The command handler or operation response.</returns>
        public static string Process(string input)
        {
            string[] split = input.Split(' ');
            if (split.Length == 0)
                throw new InvalidArgumentException("Invalid command");

            string command = split[0];

            object[] args = (object[])split.Skip(1).ToArray();

            if (m_dCommands.ContainsKey(command))
                return m_dCommands[command](args);

            throw new ProcessingException("Unknown operation or invalid arguments");
        }
    }
}
