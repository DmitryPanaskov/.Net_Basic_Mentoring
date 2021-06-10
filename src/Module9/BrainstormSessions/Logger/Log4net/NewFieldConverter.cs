using System.IO;
using global::log4net.Util;

namespace BrainstormSessions.Logger.Log4net
{
    public class NewFieldConverter : PatternConverter
    {
        protected override void Convert(TextWriter writer, object state)
        {
            var ctw = writer as CsvTextWriter;

            ctw?.WriteQuote();
            writer.Write(',');
            ctw?.WriteQuote();
        }
    }
}
