namespace FileSystemVisitor.Console
{
    using System;
    using System.IO;
    using FileSystemVisitor.Enums;
    using FileSystemVisitor.Library;

    public class Program
    {
        public static void Main(string[] args)
        {
            string startPoint = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Cars"));
            var visitor = new FileSystemVisitor(startPoint, new FileSystemProcessingStrategy(), (info) => true);

            visitor.Start += (s, e) =>
            {
                Console.WriteLine("Iteration started");
            };

            visitor.Finish += (s, e) =>
            {
                Console.WriteLine("Iteration finished");
            };

            visitor.FileFinded += (s, e) =>
            {
                Console.WriteLine("\t\tFounded file: " + e.FindedItem.Name);
            };

            visitor.DirectoryFinded += (s, e) =>
            {
                Console.WriteLine("\tFounded directory: " + e.FindedItem.Name);
                if (e.FindedItem.Name == "E-class")
                {
                    e.ActionType = ActionType.SkipElement;
                }
            };

            visitor.FilteredFileFinded += (s, e) =>
            {
                Console.WriteLine("Founded filtered file: " + e.FindedItem.Name);
                if (e.FindedItem.Name == "X5")
                    e.ActionType = ActionType.ContinueSearch;
            };

            visitor.FilteredDirectoryFinded += (s, e) =>
            {
                Console.WriteLine("Founded filtered directory: " + e.FindedItem.Name);
                if (e.FindedItem.Name == "X7")
                    e.ActionType = ActionType.SkipElement;
            };

            foreach (var fileSysInfo in visitor.GetFileSystemInfoSequence())
            {
                Console.WriteLine(fileSysInfo);
            }

            Console.Read();
        }
    }
}
