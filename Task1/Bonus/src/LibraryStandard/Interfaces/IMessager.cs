namespace LibraryStandard.Interfaces
{
    /// <summary>
    /// The method outputs the greeting to the console.
    /// </summary>
    public interface IMessager
    {
        string GetGreetingFromConsoleParameters(string[] args);

        string GetGreeting(string arg);
    }
}
