using FileSystemVisitor.Enums;
using System.IO;

namespace FileSystemVisitor.Library.EventArgs
{
    public class ItemFindedEventArgs<T> : System.EventArgs
        where T : FileSystemInfo
    {
        public T FindedItem { get; set; }

        public ActionType ActionType { get; set; }
    }
}
