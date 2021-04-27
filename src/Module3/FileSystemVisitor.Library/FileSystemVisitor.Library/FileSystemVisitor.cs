using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystemVisitor.Library.Enums;
using FileSystemVisitor.Library.EventArgs;

namespace FileSystemVisitor.Library
{
    public class FileSystemVisitor
    {
        private readonly string _path;
        private DirectoryInfo _startDirectory;
        private readonly ActionType _filteredActionType;
        private Func<FileSystemInfo, bool> _filter;
        private bool isBreak = false;

        public FileSystemVisitor(string path,
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

        public virtual IEnumerable<FileSystemInfo> GetFileSystemInfoSequence()
        {
            CheckingForExistence();

            Start?.Invoke(this, new StartEventArgs());

            var fileSystemInfos = BypassingFileSystem(_startDirectory);
            foreach (var fileSystemInfo in fileSystemInfos)
            {
                BypassingFileSystem(_startDirectory);
            }

            Finish?.Invoke(this, new FinishEventArgs());

            return fileSystemInfos;
        }

        private IEnumerable<FileSystemInfo> BypassingFileSystem(DirectoryInfo directory)
        {
            var fileSystemInfos = directory.EnumerateFileSystemInfos();
            foreach (var fileSystemInfo in fileSystemInfos)
            {
                if (isBreak)
                {
                    break;
                }

                if (fileSystemInfo is FileInfo file)
                {
                    ProcessOfSearchingItem(file);
                }

                if (fileSystemInfo is DirectoryInfo dir)
                {
                    ProcessOfSearchingItem(dir);

                    foreach (var innerInfo in BypassingFileSystem(dir))
                    {
                        yield return innerInfo;
                    }

                    if (isBreak)
                    {
                        break;
                    }

                    continue;
                }

                yield return fileSystemInfo;
            }
        }

        private void ProcessOfSearchingItem<TItemInfo>(
           TItemInfo itemInfo)
           where TItemInfo : FileSystemInfo
        {
            if (_filter is null || !_filter(itemInfo))
            {
                CallEvents(itemInfo);
                return;
            }

            if (_filter(itemInfo) && _filteredActionType == ActionType.SkipElement)
            {
                return;
            }

            CallEvents(itemInfo);

            isBreak = true;
        }

        private void CallEvents<TItemInfo>(TItemInfo itemInfo)
           where TItemInfo : FileSystemInfo
        {
            if (itemInfo is FileInfo fileInfo)
            {
                FileFound?.Invoke(this, new ItemFoundEventArgs<FileInfo>() { FoundItem = fileInfo });
            }

            if (itemInfo is DirectoryInfo directoryInfo)
            {
                DirectoryFound?.Invoke(this, new ItemFoundEventArgs<DirectoryInfo>() { FoundItem = directoryInfo });
            }
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
