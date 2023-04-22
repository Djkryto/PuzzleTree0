using UnityEngine;

namespace GameConsole
{
    public abstract class ConsoleCommandMono : MonoBehaviour, IConsoleCommand
    {
        [SerializeField] private string _commandName;

        public string CommandName => _commandName;

        public abstract void ExecuteCommand(string command);
    }
}
