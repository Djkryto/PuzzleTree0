using GameConsole.Exceptions;
using System.Collections.Generic;
using UnityEngine;

namespace GameConsole
{
    public class Console : MonoBehaviour
    {
        [SerializeField] private List<ConsoleCommandMono> _comandsMono;
        [SerializeField] private List<ConsoleCommandSO> _comandsSO;
        private Dictionary<string, IConsoleCommand> _commands = new();

        private void Awake()
        {
            AddCommands(_comandsMono);
            AddCommands(_comandsSO);
        }

        public void AddCommands<CommandType>(List<CommandType> commands) where CommandType : IConsoleCommand
        {
            foreach(IConsoleCommand command in commands)
            {
                AddCommand(command);
            }
        }

        public void AddCommand(IConsoleCommand command)
        {
            _commands.Add(command.CommandName, command);
        }

        public void TryExecuteCommand(string commandName)
        {
            var splitCommand = commandName.Split(" ");
            if (_commands.ContainsKey(splitCommand[0]))
                _commands[splitCommand[0]].ExecuteCommand(commandName);
            else
                throw new CommandNotExistException("\"" + commandName + "\"");
        }
    }

}