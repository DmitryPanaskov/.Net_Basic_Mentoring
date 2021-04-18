using System.IO;
using FileSystemVisitor.Enums;

namespace FileSystemVisitor.Library.EventArgs
{
    public class ItemFoundEventArgs<T> : System.EventArgs
        where T : FileSystemInfo
    {
        public T FoundItem { get; set; }

        public ActionType ActionType { get; set; }
    }
}
