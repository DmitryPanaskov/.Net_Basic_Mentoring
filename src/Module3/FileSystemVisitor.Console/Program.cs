using System.IO;
using FileSystemVisitor.Enums;
using FileSystemVisitor.Library;

namespace FileSystemVisitor.Console
{
    using System;
    using FileSystemVisitor = Library.FileSystemVisitor;

    public class Program
    {
        public static void Main(string[] args)
        {
            string startPoint = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Cars"));
            var visitor = new FileSystemVisitor(startPoint, new FileSystemProcessingStrategy(), (filter) => true);

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
                if (e.FoundItem.Name == "E-class")
                {
                    e.ActionType = ActionType.SkipElement;
                }
            };

            visitor.FilteredFileFound += (s, e) =>
            {
                Console.WriteLine("Founded filtered file: " + e.FoundItem.Name);
                if (e.FoundItem.Name == "X5")
                {
                    e.ActionType = ActionType.ContinueSearch;
                }
            };

            visitor.FilteredDirectoryFound += (s, e) =>
            {
                Console.WriteLine("Founded filtered directory: " + e.FoundItem.Name);
                if (e.FoundItem.Name == "X7")
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
