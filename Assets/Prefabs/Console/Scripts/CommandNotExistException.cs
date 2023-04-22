using System;

namespace GameConsole.Exceptions
{
    public class CommandNotExistException : Exception
    {
        private static string message = "The command {command} does not exist!";
        private static string commandKey = "{command}";

        public CommandNotExistException(string commandName) : base(message.Replace(commandKey, commandName))
        {
        }
    }
}