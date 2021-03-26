using System;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            var userName = Console.ReadLine();
            Console.WriteLine($"Hello {userName}!");
            Console.Read();
        }
    }
}
