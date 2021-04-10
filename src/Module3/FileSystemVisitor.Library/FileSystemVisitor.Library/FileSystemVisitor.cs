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
        private ActionType _currentActionType;
        private readonly DirectoryInfo _startDirectory;
        private readonly Func<FileSystemInfo, bool> _filter;
        private readonly IFileSystemProcessingStrategy _fileSystemProcessingStrategy;

        public FileSystemVisitor(string path,
            IFileSystemProcessingStrategy fileSystemProcessingStrategy,
            Func<FileSystemInfo, bool> filter = null)
        {
            _startDirectory = new DirectoryInfo(path);
            _filter = filter;
            _fileSystemProcessingStrategy = fileSystemProcessingStrategy;
            _currentActionType = ActionType.ContinueSearch;
        }

        private ActionType CurrentActionType
        {
            get
            {
                return _currentActionType;
            }
            set
            {
                _currentActionType = value;
            }
        }

        public event EventHandler<StartEventArgs> Start;
        public event EventHandler<FinishEventArgs> Finish;
        public event EventHandler<ItemFindedEventArgs<FileInfo>> FileFinded;
        public event EventHandler<ItemFindedEventArgs<FileInfo>> FilteredFileFinded;
        public event EventHandler<ItemFindedEventArgs<DirectoryInfo>> DirectoryFinded;
        public event EventHandler<ItemFindedEventArgs<DirectoryInfo>> FilteredDirectoryFinded;

        public IEnumerable<FileSystemInfo> GetFileSystemInfoSequence()
        {
            OnEvent(Start, new StartEventArgs());
            var fileSystemInfos = BypassFileSystem(_startDirectory, CurrentActionType);
            foreach (var fileSystemInfo in fileSystemInfos)
            {
                yield return fileSystemInfo;
            }

            OnEvent(Finish, new FinishEventArgs());
        }

        private IEnumerable<FileSystemInfo> BypassFileSystem(DirectoryInfo directory, ActionType currentAction)
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
                        foreach (var innerInfo in BypassFileSystem(dir, currentAction))
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
                .ProcessItemFinded(file, _filter, FileFinded, FilteredFileFinded, OnEvent);
        }

        private ActionType ProcessDirectory(DirectoryInfo directory)
        {
            return _fileSystemProcessingStrategy
                .ProcessItemFinded(directory, _filter, DirectoryFinded, FilteredDirectoryFinded, OnEvent);
        }

        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }

        /*
        private class CurrentAction
        {
            public ActionType Action { get; set; }
            public static CurrentAction ContinueSearch
                => new CurrentAction { Action = ActionType.ContinueSearch };
        }
        */

    }
}
