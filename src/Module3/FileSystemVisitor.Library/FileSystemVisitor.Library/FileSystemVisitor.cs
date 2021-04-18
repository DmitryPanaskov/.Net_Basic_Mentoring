using System;
using System.Collections.Generic;
using System.IO;
using FileSystemVisitor.Enums;
using FileSystemVisitor.Library.EventArgs;
using FileSystemVisitor.Library.Interfaces;

namespace FileSystemVisitor.Library
{
    public class FileSystemVisitor
    {
        private readonly DirectoryInfo _startDirectory;
        private readonly Func<FileSystemInfo, bool> _filter;
        private readonly IFileSystemProcessingStrategy _fileSystemProcessingStrategy;

        public FileSystemVisitor(string path, IFileSystemProcessingStrategy fileSystemProcessingStrategy, Func<FileSystemInfo, bool> filter = null)
        {
            _startDirectory = new DirectoryInfo(path);
            _filter = filter;
            _fileSystemProcessingStrategy = fileSystemProcessingStrategy;
            CurrentActionType = ActionType.ContinueSearch;
        }

        public event EventHandler<StartEventArgs> Start;

        public event EventHandler<FinishEventArgs> Finish;

        public event EventHandler<ItemFoundEventArgs<FileInfo>> FileFound;

        public event EventHandler<ItemFoundEventArgs<FileInfo>> FilteredFileFound;

        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> DirectoryFound;

        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> FilteredDirectoryFound;

        private ActionType CurrentActionType { get; }

        public IEnumerable<FileSystemInfo> GetFileSystemInfoSequence()
        {
            OnEvent(Start, new StartEventArgs());
            var fileSystemInfos = BypassingFileSystem(_startDirectory, CurrentActionType);
            foreach (var fileSystemInfo in fileSystemInfos)
            {
                yield return fileSystemInfo;
            }

            OnEvent(Finish, new FinishEventArgs());
        }

        private IEnumerable<FileSystemInfo> BypassingFileSystem(DirectoryInfo directory, ActionType currentAction)
        {
            var fileSystemInfos = directory.EnumerateFileSystemInfos();
            foreach (var fileSystemInfo in fileSystemInfos)
            {
                if (fileSystemInfo is FileInfo file)
                {
                    currentAction = ProcessFile(file);
                }

                if (fileSystemInfo is DirectoryInfo dir)
                {
                    currentAction = ProcessDirectory(dir);
                    if (currentAction == ActionType.ContinueSearch)
                    {
                        yield return dir;
                        foreach (var innerInfo in BypassingFileSystem(dir, currentAction))
                        {
                            yield return innerInfo;
                        }

                        continue;
                    }
                }

                if (currentAction == ActionType.StopSearch)
                {
                    yield break;
                }

                yield return fileSystemInfo;
            }
        }

        private ActionType ProcessFile(FileInfo file)
        {
            return _fileSystemProcessingStrategy
                .ProcessOfSearchingItem(file, _filter, FileFound, FilteredFileFound, OnEvent);
        }

        private ActionType ProcessDirectory(DirectoryInfo directory)
        {
            return _fileSystemProcessingStrategy
                .ProcessOfSearchingItem(directory, _filter, DirectoryFound, FilteredDirectoryFound, OnEvent);
        }

        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }
    }
}
