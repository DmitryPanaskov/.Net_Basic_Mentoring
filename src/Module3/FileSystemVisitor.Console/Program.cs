using System.IO;
using FileSystemVisitor.Library;
using FileSystemVisitor.Library.Enums;

namespace FileSystemVisitor.Console
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            string startPoint = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Cars"));

            var visitor = new Library.FileSystemVisitor(startPoint);

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
                Console.WriteLine("\t\tFounded filtered file: " + e.FoundItem.Name);
                if (e.FoundItem.Name == "e36")
                {
                    e.ActionType = ActionType.SkipElement;
                }
            };

            visitor.FilteredDirectoryFound += (s, e) =>
            {
                Console.WriteLine("\tFounded filtered directory: " + e.FoundItem.Name);
                if (e.FoundItem.Name == "BMW")
                {
                    e.ActionType = ActionType.SkipElement;
                }
            };

            foreach (var fileSysInfo in visitor.GetFileSystemInfoSequence())
            {
            }

            Console.Read();
        }
    }
}
