using System;
using System.Collections.Generic;
using System.IO;
using FileSystemVisitor.Library.Enums;
using FileSystemVisitor.Library.EventArgs;

namespace FileSystemVisitor.Library
{
    public class FileSystemVisitor
    {
        private readonly string _path;
        private readonly Func<FileSystemInfo, bool> _filter;
        private DirectoryInfo _startDirectory;
        private bool isBreak;

        public FileSystemVisitor(
            string path,
            Func<FileSystemInfo, bool> filter = null)
        {
            _path = path;
            _filter = filter;
        }

        public event EventHandler<StartEventArgs> Start;

        public event EventHandler<FinishEventArgs> Finish;

        public event EventHandler<ItemFoundEventArgs<FileInfo>> FileFound;

        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> DirectoryFound;

        public event EventHandler<ItemFoundEventArgs<FileInfo>> FilteredFileFound;

        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> FilteredDirectoryFound;

        public virtual IEnumerable<FileSystemInfo> GetFileSystemInfoSequence()
        {
            CheckingForExistence();

            Start?.Invoke(this, new StartEventArgs());

            foreach (var fileSystemInfo in BypassingFileSystem(_startDirectory))
            {
                yield return fileSystemInfo;
            }

            Finish?.Invoke(this, new FinishEventArgs());
        }

        private IEnumerable<FileSystemInfo> BypassingFileSystem(DirectoryInfo directory)
        {
            var fileSystemInfos = directory.EnumerateFileSystemInfos();
            var result = ActionType.ContinueSearch;
            foreach (var fileSystemInfo in fileSystemInfos)
            {
                CallEvents(fileSystemInfo);

                if (isBreak)
                {
                    break;
                }

                result = ProcessOfSearchingItem(fileSystemInfo);

                switch (result)
                {
                    case ActionType.ContinueSearch:
                        yield return fileSystemInfo;
                        break;
                    case ActionType.SkipElement:
                        break;
                    case ActionType.StopSearch:
                        isBreak = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (fileSystemInfo is DirectoryInfo dir)
                {
                    if (result == ActionType.ContinueSearch)
                    {
                        foreach (var innerInfo in BypassingFileSystem(dir))
                        {
                            yield return innerInfo;
                        }
                    }
                }
            }
        }

        private ActionType ProcessOfSearchingItem<TItemInfo>(
           TItemInfo itemInfo)
           where TItemInfo : FileSystemInfo
        {
            if (_filter is null || !_filter(itemInfo))
            {
                return ActionType.ContinueSearch;
            }

            return CallFilteredEvents(itemInfo);
        }

        private void CallEvents<TItemInfo>(TItemInfo itemInfo)
           where TItemInfo : FileSystemInfo
        {
            if (itemInfo is FileInfo fileInfo)
            {
                var args = new ItemFoundEventArgs<FileInfo>
                {
                    FoundItem = fileInfo,
                    ActionType = ActionType.ContinueSearch
                };

                FileFound?.Invoke(this, args);
            }

            if (itemInfo is DirectoryInfo directoryInfo)
            {
                var args = new ItemFoundEventArgs<DirectoryInfo>
                {
                    FoundItem = directoryInfo,
                    ActionType = ActionType.ContinueSearch
                };

                DirectoryFound?.Invoke(this, args);
            }
        }

        private ActionType CallFilteredEvents<TItemInfo>(TItemInfo itemInfo)
            where TItemInfo : FileSystemInfo
        {
            if (itemInfo is FileInfo fileInfo)
            {
                var args = new ItemFoundEventArgs<FileInfo>
                {
                    FoundItem = fileInfo,
                    ActionType = ActionType.ContinueSearch
                };

                FilteredFileFound?.Invoke(this, args);
                return args.ActionType;
            }

            if (itemInfo is DirectoryInfo directoryInfo)
            {
                var args = new ItemFoundEventArgs<DirectoryInfo>
                {
                    FoundItem = directoryInfo,
                    ActionType = ActionType.ContinueSearch
                };

                FilteredDirectoryFound?.Invoke(this, args);
                return args.ActionType;
            }

            throw new ArgumentOutOfRangeException();
        }

        private void CheckingForExistence()
        {
            if (Directory.Exists(_path))
            {
                _startDirectory = new DirectoryInfo(_path);
            }
            else
            {
                throw new DirectoryNotFoundException($"Directory not found: {_path}");
            }
        }
    }
}
