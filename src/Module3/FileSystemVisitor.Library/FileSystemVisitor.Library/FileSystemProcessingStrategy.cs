using System;
using System.IO;
using FileSystemVisitor.Enums;
using FileSystemVisitor.Library.EventArgs;
using FileSystemVisitor.Library.Interfaces;

namespace FileSystemVisitor.Library
{
    public class FileSystemProcessingStrategy : IFileSystemProcessingStrategy
    {
        /// <inheritdoc/>
        public ActionType ProcessOfSearchingItem<TItemInfo>(
            TItemInfo itemInfo,
            Func<FileSystemInfo, bool> filter,
            EventHandler<ItemFoundEventArgs<TItemInfo>> itemFound,
            EventHandler<ItemFoundEventArgs<TItemInfo>> filteredItemFound,
            Action<EventHandler<ItemFoundEventArgs<TItemInfo>>, ItemFoundEventArgs<TItemInfo>> eventEmitter)
            where TItemInfo : FileSystemInfo
        {
            var args = new ItemFoundEventArgs<TItemInfo>
            {
                FoundItem = itemInfo,
                ActionType = ActionType.ContinueSearch
            };

            eventEmitter(itemFound, args);

            if (args.ActionType != ActionType.ContinueSearch || filter is null)
            {
                return args.ActionType;
            }

            if (filter(itemInfo))
            {
                args = new ItemFoundEventArgs<TItemInfo>
                {
                    FoundItem = itemInfo,
                    ActionType = ActionType.ContinueSearch
                };

                eventEmitter(filteredItemFound, args);

                return args.ActionType;
            }

            return ActionType.SkipElement;
        }
    }
}
