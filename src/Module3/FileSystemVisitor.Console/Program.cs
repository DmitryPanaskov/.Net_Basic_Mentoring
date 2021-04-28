using System.Collections.Generic;
using System.IO;
using FileSystemVisitor.Library.Enums;

namespace FileSystemVisitor.Console
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            string startPoint = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Cars"));

            var visitor = new Library.FileSystemVisitor(startPoint, x=>x.Name == "e39.txt", ActionType.SkipElement);

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
                if (e.FoundItem.Name == "q7.txt")
                {
                    e.ActionType = ActionType.SkipElement;
                }
            };

            visitor.FilteredDirectoryFound += (s, e) =>
            {
                Console.WriteLine("\tFounded filtered directory: " + e.FoundItem.Name);
                if (e.FoundItem.Name == "vw")
                {
                    e.ActionType = ActionType.SkipElement;
                }
            };

            var list = new List<FileSystemInfo>();

            foreach (var fileSysInfo in visitor.GetFileSystemInfoSequence())
            {
                list.Add(fileSysInfo);
            }

            // --------------------- normalized directory tree ----------------
            Console.WriteLine("\n\n\n================ Normalized Directory Tree ================");
            Console.WriteLine("Iteration started");
            foreach (var item in list)
            {
                if (item is DirectoryInfo)
                {
                    Console.WriteLine(item.Name);
                }

                if (item is FileInfo)
                {
                    Console.WriteLine("\t" + item.Name);
                }
            }
            Console.WriteLine("Iteration finished");
            Console.WriteLine("===========================================================");
            Console.Read();
        }
    }
}
