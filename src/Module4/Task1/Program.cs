using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter a string and press ENTER. ");
            Console.WriteLine("Enter an 'exit' and press ENTER for EXIT. ");
            while (true)
            {
                var value = Console.ReadLine();
                if (value == "exit")
                {
                    break;
                }

                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("The string must not be empty.");
                }
                else
                {
                    Console.WriteLine(value[0]);
                }
            }
        }
    }
}