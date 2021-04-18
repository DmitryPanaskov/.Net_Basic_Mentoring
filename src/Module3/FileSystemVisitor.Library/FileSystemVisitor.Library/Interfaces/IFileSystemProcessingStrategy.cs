using FileSystemVisitor.Enums;
using FileSystemVisitor.Library.EventArgs;
using System;
using System.IO;

namespace FileSystemVisitor.Library.Interfaces
{
    public interface IFileSystemProcessingStrategy
    {
        /// <summary>
        /// Search for folders and files by the specified filters.
        /// </summary>
        /// <typeparam name="TItemInfo"></typeparam>
        /// <param name="itemInfo">Folder or file as object type.</param>
        /// <param name="filter">Filter to restrict search.</param>
        /// <param name="itemFound">The found item (folder or file).</param>
        /// <param name="filteredItemFound">The found item that falls under the filter(folder or file).</param>
        /// <param name="eventEmitter">Calling the event.</param>
        /// <returns>Returns the action type.</returns>
        ActionType ProcessOfSearchingItem<TItemInfo>(
            TItemInfo itemInfo,
            Func<FileSystemInfo, bool> filter,
            EventHandler<ItemFoundEventArgs<TItemInfo>> itemFound,
            EventHandler<ItemFoundEventArgs<TItemInfo>> filteredItemFound,
            Action<EventHandler<ItemFoundEventArgs<TItemInfo>>, ItemFoundEventArgs<TItemInfo>> eventEmitter)
            where TItemInfo : FileSystemInfo;
    }
}
