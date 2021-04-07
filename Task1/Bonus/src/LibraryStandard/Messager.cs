namespace LibraryStandard
{
    using System;
    using LibraryStandard.Interfaces;
    using LibraryStandard.Options;
    using MatthiWare.CommandLine;

    public class Messager : IMessager
    {
        /// <inheritdoc/>
        public string GetGreetingWithDate(string[] args)
        {
            var currentDateTime = DateTimeOffset.Now;
            var parser = new CommandLineParser<ProgramOptions>(new CommandLineParserOptions());
            var values = parser.Parse(args);

            if (values.HasErrors)
            {
                Console.Error.WriteLine("Parsing has errors...");
            }

            return $"{currentDateTime} Hello {values}!";
        }
    }
}
