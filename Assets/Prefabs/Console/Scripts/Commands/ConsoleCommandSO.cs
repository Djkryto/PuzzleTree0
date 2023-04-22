using UnityEngine;

namespace GameConsole
{
    public abstract class ConsoleCommandSO : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string _commandName;

        public string CommandName => _commandName;

        public abstract void ExecuteCommand(string command);
    }
}