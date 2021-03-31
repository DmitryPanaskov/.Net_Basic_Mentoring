using System;
using LibraryStandard;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter your name as a argument command line.");
            }

            var messager = new Messager();

            foreach (var item in args)
            {
                Console.WriteLine(messager.GetGreetingWithDate(item));
            }

            Console.Read();
        }
    }
}
