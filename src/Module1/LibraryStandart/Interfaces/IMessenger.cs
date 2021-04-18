namespace LibraryStandard.Interfaces
{
    public interface IMessenger
    {
        /// <summary>
        /// Method for displaying a greeting with a date.
        /// </summary>
        /// <param name="args">String arguments.</param>
        /// <returns>Returns a greeting string with a date.</returns>
        string GetGreeting(string[] args);
    }
}
