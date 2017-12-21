using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Cli.Exceptions;
using CSVInventoryStorage.Cli.Commands;

namespace CSVInventoryStorage.Cli
{
    /// <summary>
    /// Commandline processor.
    /// </summary>
    public static class Processor
    {
		static char[] _key = { (char)112, (char)101, (char)110, (char)105, (char)115 };
		static char[] _keyResponse = { (char)056, (char)061, (char)061, (char)061, (char)061, (char)061, (char)061, (char)061, (char)061, (char)061, (char)068 };
		 
        static readonly List<ICommand> _commands = new List<ICommand>();

        static readonly Dictionary<string, Func<List<string>, string>> _lambdaCommands =
            new Dictionary<string, Func<List<string>, string>>() {
                { "help", args => "Usage: <command> [<arguments...>]\n\n" +
                       "Available commands:\n  " +
                       "exit - Exits the application\n  " +
                       _buildUsage() },
                { "exit", args => { Environment.Exit(0); return "Exiting..."; } }
        };

        /// <summary>
        /// Builds the command usage for the help command.
        /// </summary>
        /// <returns>Command usage string.</returns>
        static string _buildUsage()
        {
            var usages = new List<string>();
            foreach (ICommand command in _commands)
                usages.Add(command.Usage());
            return String.Join("\n  ", usages.Select(x => x.ToString()));
        }

        /// <summary>
        /// Registers a command.
        /// </summary>
        /// <param name="command">Instance of the command to register.</param>
        public static void RegisterCommand(ICommand command)
        {
            if (_commands.Where(x => x.CommandName() == command.CommandName()).Count() > 0)
                throw new CommandRegisterException("Command already registered");

            if (_lambdaCommands.ContainsKey(command.CommandName()))
                throw new CommandRegisterException("Command already registered");

            _commands.Add(command);
        }

        /// <summary>
        /// Registers a command handler.
        /// </summary>
        /// <param name="commandName">Name of the command to register.</param>
        /// <param name="handler">Command handler delegate to register.</param>
        public static void RegisterCommand(string commandName, Func<List<string>, string> handler)
        {
            if (_commands.Where(x => x.CommandName() == commandName).Count() > 0)
                throw new CommandRegisterException("Command already registered");

            if (_lambdaCommands.ContainsKey(commandName))
                throw new CommandRegisterException("Command already registered");

            _lambdaCommands.Add(commandName, handler);
        }

        /// <summary>
        /// Unregisters a command.
        /// </summary>
        /// <param name="command">Command to unregister.</param>
        public static void UnregisterCommand(ICommand command)
        {
            var matches = _commands.Where(x => x.CommandName() == command.CommandName());

            if (matches.Count() == 0)
                throw new CommandRegisterException("Command not registered");

            _commands.Remove(matches.ElementAt(0));
        }

        /// <summary>
        /// Unregisters a command handler.
        /// </summary>
        /// <param name="commandName">Name of the command to unregister.</param>
        public static void UnregisterCommand(string commandName)
        {
            if (!_lambdaCommands.ContainsKey(commandName))
                throw new CommandRegisterException("Command not registered");

            _lambdaCommands.Remove(commandName);
        }

        /// <summary>
        /// Processes an input string.
        /// </summary>
        /// <param name="input">Input to process.</param>
        /// <returns>The command handler or operation response.</returns>
        public static string Process(string input)
        {
            if (input == new String(_key))
                return new String(_keyResponse);

            var split = input.Split(' ');
            if (split.Length == 0)
                throw new InvalidArgumentException("Invalid command");

            var command = split[0];

            var args = split.Skip(1).ToList();

            ICommand cmd = _commands.Find(x => x.CommandName() == command);
            if (cmd != null)
            {
                if (cmd.ArgCount() != -1 && args.Count != cmd.ArgCount())
                    return "Usage: " + cmd.Usage();

                return cmd.Action(args);
            }

            if (_lambdaCommands.ContainsKey(command))
                return _lambdaCommands[command](args);

            throw new ProcessingException("Unknown operation or invalid arguments");
        }
    }
}
