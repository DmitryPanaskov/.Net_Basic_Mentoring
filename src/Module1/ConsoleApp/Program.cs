using System;
using LibraryStandard;
using LibraryStandard.Interfaces;
using MatthiWare.CommandLine;

namespace ConsoleApp
{
    public static class Program
    {
        private static IMessenger _messenger;

        public static void Main(string[] args)
        {
            _messenger = new Messenger();
            var parser = new CommandLineParser<ProgramOptions>(new CommandLineParserOptions());
            var values = parser.Parse(args);

            if (values.HasErrors)
            {
                Console.Error.WriteLine("Parsing has errors...");
            }

            Console.WriteLine(_messenger.GetGreeting(new[]
            {
                values.Result.FirstName,
                values.Result.LastName,
            }));

            Console.Read();
        }
    }
}
