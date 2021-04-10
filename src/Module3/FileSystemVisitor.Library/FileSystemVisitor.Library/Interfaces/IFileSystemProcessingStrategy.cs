using FileSystemVisitor.Enums;
using FileSystemVisitor.Library.EventArgs;
using System;
using System.IO;

namespace FileSystemVisitor.Library.Interfaces
{
    public interface IFileSystemProcessingStrategy
    {
        ActionType ProcessItemFinded<TItemInfo>(
            TItemInfo itemInfo,
            Func<FileSystemInfo, bool> filter,
            EventHandler<ItemFindedEventArgs<TItemInfo>> itemFinded,
            EventHandler<ItemFindedEventArgs<TItemInfo>> filteredItemFinded,
            Action<EventHandler<ItemFindedEventArgs<TItemInfo>>, ItemFindedEventArgs<TItemInfo>> eventEmitter)
            where TItemInfo : FileSystemInfo;
    }
}
