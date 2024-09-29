using Cuphead.Interfaces;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Commands
{
    internal class CommandManager 
    {

        private readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public CommandManager(Game1 game)
        {
            // Register all commands in the constructor
            _commands["Reset"] = new Reset(game);
            _commands["Exit"] = new Exit(game);
            _commands["Update"] = new Exit(game);

        }

        // Method to get and execute a command by key
        public ICommand GetCommand(string commandName)
        {
            if (_commands.ContainsKey(commandName))
            {
                return _commands[commandName];
            }
            throw new Exception($"Command {commandName} not found");
        }


    }
}
