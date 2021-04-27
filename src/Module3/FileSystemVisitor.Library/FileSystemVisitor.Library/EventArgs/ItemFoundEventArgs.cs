using System.IO;

namespace FileSystemVisitor.Library.EventArgs
{
    using global::FileSystemVisitor.Library.Enums;

    public class ItemFoundEventArgs<T> : System.EventArgs
        where T : FileSystemInfo
    {
        public T FoundItem { get; set; }

        public ActionType ActionType { get; set; }
    }
}
