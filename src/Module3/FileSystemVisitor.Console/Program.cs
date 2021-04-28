using System.Collections.Generic;
using System.IO;
using FileSystemVisitor.Library.Enums;

namespace FileSystemVisitor.Console
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            string startPoint = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Cars"));

            var visitor = new Library.FileSystemVisitor(startPoint, x => x.Name == "q5.txt");

            visitor.Start += (s, e) =>
            {
                Console.WriteLine("Iteration started");
            };

            visitor.Finish += (s, e) =>
            {
                Console.WriteLine("Iteration finished");
            };

            visitor.FileFound += (s, e) =>
            {
                Console.WriteLine("\t\tFounded file: " + e.FoundItem.Name);
            };

            visitor.DirectoryFound += (s, e) =>
            {
                Console.WriteLine("\tFounded directory: " + e.FoundItem.Name);
            };


            visitor.FilteredFileFound += (s, e) =>
            {
                e.ActionType = ActionType.StopSearch;
            };

            visitor.FilteredDirectoryFound += (s, e) =>
            {
                e.ActionType = ActionType.SkipElement;
            };


            var list = visitor.GetFileSystemInfoSequence().ToList();

            Console.WriteLine("\n\nNormalized Directory Tree");
            Console.WriteLine("Iteration started");
            foreach (var item in list)
            {
                if (item is DirectoryInfo)
                {
                    Console.WriteLine("\tFounded filtered directory: " + item.Name);
                }

                if (item is FileInfo)
                {
                    Console.WriteLine("\t\tFounded filtered file: " + item.Name);
                }
            }
            Console.WriteLine("Iteration finished");
            Console.Read();
        }
    }
}
