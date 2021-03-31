using System;

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

            foreach (var item in args)
            {
                Console.WriteLine($"Hello {item}!");
            }

            Console.Read();
        }
    }
}
