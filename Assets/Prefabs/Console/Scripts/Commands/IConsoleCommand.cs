namespace GameConsole
{
    public interface IConsoleCommand
    {
        public string CommandName { get; }

        public void ExecuteCommand(string command);
    }
}
