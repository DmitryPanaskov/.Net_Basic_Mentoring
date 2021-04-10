namespace LibraryStandard
{
    using System;
    using LibraryStandard.Interfaces;
    using LibraryStandard.Options;
    using MatthiWare.CommandLine;

    public class Messager : IMessager
    {
        /// <inheritdoc/>
        public string GetGreetingFromConsoleParameters(string[] args)
        {
            var parser = new CommandLineParser<ProgramOptions>(new CommandLineParserOptions());
            var values = parser.Parse(args);

            if (values.HasErrors)
            {
                Console.Error.WriteLine("Parsing has errors...");
            }

            return $"{GetCurrentDate()} Hello {values.Result.FirstName} {values.Result.LastName}!";
        }

        /// <inheritdoc/>
        public string GetGreeting(string arg)
        {
            return $"{GetCurrentDate()} Hello {arg}!";
        }

        private DateTimeOffset GetCurrentDate()
        {
            return DateTimeOffset.Now;
        }
    }
}
