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
        private readonly ActionType _filteredActionType;
        private readonly Func<FileSystemInfo, bool> _filter;
        private DirectoryInfo _startDirectory;
        private bool isBreak;

        public FileSystemVisitor(
            string path,
            Func<FileSystemInfo, bool> filter = null,
            ActionType filteredActionType = ActionType.ContinueSearch)
        {
            _path = path;
            _filter = filter;
            _filteredActionType = filteredActionType;
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

            var fileSystemInfo = BypassingFileSystem(_startDirectory);

            Finish?.Invoke(this, new FinishEventArgs());

            return fileSystemInfo;
        }

        private IEnumerable<FileSystemInfo> BypassingFileSystem(DirectoryInfo directory)
        {
            var fileSystemInfos = directory.EnumerateFileSystemInfos();
            var result = ActionType.ContinueSearch;
            foreach (var fileSystemInfo in fileSystemInfos)
            {
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
                    foreach (var innerInfo in BypassingFileSystem(dir))
                    {
                        yield return innerInfo;
                    }
                }
            }
        }

        private ActionType ProcessOfSearchingItem<TItemInfo>(
           TItemInfo itemInfo)
           where TItemInfo : FileSystemInfo
        {
            CallEvents(itemInfo);

            if (_filter is null || !_filter(itemInfo))
            {
                return ActionType.ContinueSearch;
            }

            switch (_filteredActionType)
            {
                case ActionType.ContinueSearch:
                    CallFilteredFileFound(itemInfo);
                    return ActionType.ContinueSearch;
                case ActionType.SkipElement:
                    return ActionType.SkipElement;
                case ActionType.StopSearch:
                    isBreak = true;
                    return ActionType.StopSearch;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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

        private ActionType CallFilteredFileFound<TItemInfo>(TItemInfo itemInfo)
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
