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
        private static List<ICommand> m_lCommands = new List<ICommand>();

        private static Dictionary<string, Func<object[], string>> m_dLambdaCommands = new Dictionary<string, Func<object[], string>>() {
            { "help", (object[] args) => { return "Usage: <command|operator|function> [<operands...>]\nExample input: + 5 10 15\n\nAvailable commands:\n  help - Displays available commands\n  exit - Exits SimpleCalc\n\nAvailable operators:\n  + - Adds two or more operands\n  - - Subtracts two or more operands\n  * - Multiplies two or more numbers\n  / - Divides two or more numbers\n\nAvailable functions:\n  sin - Calculates the sinus of a number\n  cos - Calculates the cosinus of a number\n  tan - Calculates the tangent of a number\n  deg2rad - Converts an angle from degrees to radians\n\nAvailable placeholders:\n  &l - Reference to the last result"; } },
            { "exit", (object[] args) => { Environment.Exit(0); return "Exiting..."; } }
        };

        /// <summary>
        /// Registers a command.
        /// </summary>
        /// <param name="command">Instance of the command to register.</param>
        public static void RegisterCommand(ICommand command)
        {
            if (m_lCommands.Contains(command))
                throw new CommandRegisterException("Command already registered");

            m_lCommands.Add(command);
        }

        /// <summary>
        /// Registers a command handler.
        /// </summary>
        /// <param name="commandName">Name of the command to register.</param>
        /// <param name="handler">Command handler delegate to register.</param>
        public static void RegisterCommand(string commandName, Func<object[], string> handler)
        {
            if (m_dLambdaCommands.ContainsKey(commandName))
                throw new CommandRegisterException("Command already registered");

            m_dLambdaCommands.Add(commandName, handler);
        }

        /// <summary>
        /// Unregisters a command.
        /// </summary>
        /// <param name="command">Command to unregister.</param>
        public static void UnregisterCommand(ICommand command)
        {
            if (!m_lCommands.Contains(command))
                throw new CommandRegisterException("Command not registered");

            m_lCommands.Remove(command);
        }

        /// <summary>
        /// Unregisters a command handler.
        /// </summary>
        /// <param name="commandName">Name of the command to unregister.</param>
        public static void UnregisterCommand(string commandName)
        {
            if (!m_dLambdaCommands.ContainsKey(commandName))
                throw new CommandRegisterException("Command not registered");

            m_dLambdaCommands.Remove(commandName);
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

            ICommand cmd = m_lCommands.Find(x => x.commandName() == command);
            if (cmd != null)
            {
                if (cmd.argCount() != -1 && args.Length != cmd.argCount())
                    return cmd.usage();
                return cmd.action(args);
            }

            if (m_dLambdaCommands.ContainsKey(command))
                return m_dLambdaCommands[command](args);

            throw new ProcessingException("Unknown operation or invalid arguments");
        }
    }
}
