using System;
using LibraryStandard;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            var userName = Console.ReadLine();
            var messager = new Messager();
            Console.WriteLine(messager.GetGreetingWithDate(userName));
            Console.Read();
        }
    }
}
