using System;
using UnityEngine;

namespace GameConsole
{
    [Serializable]
    public abstract class ConsoleCommand : IConsoleCommand
    {
        [SerializeField] private string _commandName;

        public string CommandName => _commandName;

        public ConsoleCommand(string commandName)
        {
            _commandName = commandName;
        }

        public abstract void ExecuteCommand(string command);
    }
}
