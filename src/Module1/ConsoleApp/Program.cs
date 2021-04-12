using System;
using LibraryStandard;
using LibraryStandard.Interfaces;
using MatthiWare.CommandLine;

namespace ConsoleApp
{
    public static class Program
    {
        private static IMessanger _messanger;

        public static void Main(string[] args)
        {
            _messanger = new Messanger();
            var parser = new CommandLineParser<ProgramOptions>(new CommandLineParserOptions());
            var values = parser.Parse(args);

            if (values.HasErrors)
            {
                Console.Error.WriteLine("Parsing has errors...");
            }

            Console.WriteLine(_messanger.GetGreeting(new[]
            {
                values.Result.FirstName,
                values.Result.LastName,
            }));

            Console.Read();
        }
    }
}
