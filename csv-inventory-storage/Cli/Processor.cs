﻿using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Cli.Exceptions;
using CSVInventoryStorage.Cli.Commands;

namespace CSVInventoryStorage.Cli
{
    public static class Processor
    {
        static readonly char[] Key = { (char)112, (char)101, (char)110, (char)105, (char)115 };
       
        static readonly char[] KeyResponse = { (char)056, (char)061, (char)061, (char)061, (char)061, (char)061, (char)061, (char)061, (char)061, (char)061, (char)068 };

        /// <summary>
        /// List of registered commands.
        /// </summary>
        static readonly List<ICommand> Commands = new List<ICommand>();

        /// <summary>
        /// Dictionary of registered lambda commands.
        /// </summary>
        static readonly Dictionary<string, Func<List<string>, string>> LambdaCommands =
            new Dictionary<string, Func<List<string>, string>> {
                { "help", args => "Usage: <command> [<arguments...>]\n\n" +
                       "Available commands:\n  " +
                       "exit - Exits the application\n  " +
                       BuildHelp() },
                { "exit", args => { Environment.Exit(0); return "Exiting..."; } }
        };

        /// <summary>
        /// Builds the command usage for the help command.
        /// </summary>
        /// <returns>Command help string.</returns>
        static string BuildHelp() => string.Join("\n  ", Commands.Select(command => command.Usage() + " - " + command.Description()).ToList());

        /// <summary>
        /// Registers a command.
        /// </summary>
        /// <param name="command">Instance of the command to register.</param>
        public static void RegisterCommand(ICommand command)
        {
            if (Commands.Any(x => x.CommandName() == command.CommandName()))
                throw new CommandRegisterException("Command already registered");

            if (LambdaCommands.ContainsKey(command.CommandName()))
                throw new CommandRegisterException("Command already registered");

            Commands.Add(command);
        }

        /// <summary>
        /// Registers a command handler.
        /// </summary>
        /// <param name="commandName">Name of the command to register.</param>
        /// <param name="handler">Command handler delegate to register.</param>
        public static void RegisterCommand(string commandName, Func<List<string>, string> handler)
        {
            if (Commands.Any(x => x.CommandName() == commandName))
                throw new CommandRegisterException("Command already registered");

            if (LambdaCommands.ContainsKey(commandName))
                throw new CommandRegisterException("Command already registered");

            LambdaCommands.Add(commandName, handler);
        }

        /// <summary>
        /// Unregisters a command.
        /// </summary>
        /// <param name="command">Command to unregister.</param>
        public static void UnregisterCommand(ICommand command)
        {
            var matches = Commands.Where(x => x.CommandName() == command.CommandName());

            if (!matches.Any())
                throw new CommandRegisterException("Command not registered");

            Commands.Remove(matches.ElementAt(0));
        }

        /// <summary>
        /// Unregisters a command handler.
        /// </summary>
        /// <param name="commandName">Name of the command to unregister.</param>
        public static void UnregisterCommand(string commandName)
        {
            if (!LambdaCommands.ContainsKey(commandName))
                throw new CommandRegisterException("Command not registered");

            LambdaCommands.Remove(commandName);
        }

        /// <summary>
        /// Processes an input string.
        /// </summary>
        /// <param name="input">Input to process.</param>
        /// <returns>The command handler or operation response.</returns>
        public static string Process(string input)
        {
            if (input == new string(Key))
				return new string(KeyResponse);

			var split = input.Split(' ');
            if (split.Length == 0)
                throw new InvalidArgumentException("Invalid command");

            var command = split[0];

            var args = split.Skip(1).ToList();

            ICommand cmd = Commands.Find(x => x.CommandName() == command);
            if (cmd != null)
            {
                if (cmd.ArgCount() != -1 && args.Count != cmd.ArgCount())
                    return "Usage: " + cmd.Usage();

                return cmd.Action(args);
            }

            if (LambdaCommands.ContainsKey(command))
                return LambdaCommands[command](args);

            throw new ProcessingException("Unknown operation or invalid arguments");
        }
    }
}
