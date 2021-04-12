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
            var visitor = new FileSystemVisitor(startPoint, new FileSystemProcessingStrategy(), x=>x.Name.Equals("VAG"));
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
                if (e.FindedItem.Name.Length == 4)
                {
                    e.ActionType = ActionType.StopSearch;
                }
            };

            visitor.FilteredFileFinded += (s, e) =>
            {
                Console.WriteLine("Founded filtered file: " + e.FindedItem.Name);
                if (e.FindedItem.Name == "")
                    e.ActionType = ActionType.StopSearch;
            };

            visitor.FilteredDirectoryFinded += (s, e) =>
            {
                Console.WriteLine("Founded filtered directory: " + e.FindedItem.Name);
                if (e.FindedItem.Name.Length == 4)
                    e.ActionType = ActionType.StopSearch;
            };

            foreach (var fileSysInfo in visitor.GetFileSystemInfoSequence())
            {
                //Console.WriteLine(fileSysInfo);
            }

            Console.Read();
        }
    }
}
