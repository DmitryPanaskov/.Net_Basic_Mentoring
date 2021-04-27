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

            // var visitor = new Library.FileSystemVisitor(startPoint, x => x.Name == "w211.txt", ActionType.SkipElement);
            // var visitor = new Library.FileSystemVisitor(startPoint, x => x.Name == "w211.txt", ActionType.StopSearch);
            // var visitor = new Library.FileSystemVisitor(startPoint, x => x.Name == "BMW", ActionType.SkipElement);
            // var visitor = new Library.FileSystemVisitor(startPoint, x => x.Name == "e39.txt", ActionType.StopSearch);
             var visitor = new Library.FileSystemVisitor(startPoint, x => x.Name == "E-class", ActionType.StopSearch);
            // var visitor = new Library.FileSystemVisitor(startPoint, null, ActionType.StopSearch);
            // var visitor = new Library.FileSystemVisitor(startPoint);

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

            /*
            visitor.FilteredFileFound += (s, e) =>
            {
                if (e.FoundItem.Name == "e36")
                {
                    e.ActionType = ActionType.StopSearch;
                    Console.WriteLine("Founded skipped file: " + e.FoundItem.Name);
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
            */
            visitor.GetFileSystemInfoSequence();
            /*foreach (var fileSysInfo in visitor.GetFileSystemInfoSequence())
            {
            }
            */
            Console.Read();
        }
    }
}
