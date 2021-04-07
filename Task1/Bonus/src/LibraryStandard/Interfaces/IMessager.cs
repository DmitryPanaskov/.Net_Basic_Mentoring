namespace LibraryStandard.Interfaces
{
    /// <summary>
    /// The method outputs the greeting to the console.
    /// </summary>
    public interface IMessager
    {
       string GetGreetingWithDate(string[] args);
    }
}
