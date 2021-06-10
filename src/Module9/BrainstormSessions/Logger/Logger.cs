using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace BrainstormSessions.Logger
{
    public class Logger
    {
        private const string Path = @"log4net.config";

        public static ILog Log { get; } = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void InitLogger()
        {
            var repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(Path);

            XmlConfigurator.Configure(repository, fileInfo);
        }
    }
}
