using System.IO;
using global::log4net.Core;
using global::log4net.Layout;

namespace BrainstormSessions.Logger.Log4net
{
    public class CsvPatternLayout : PatternLayout
    {
        public override void ActivateOptions()
        {
            this.AddConverter("newfield", typeof(NewFieldConverter));
            this.AddConverter("endrow", typeof(EndRowConverter));
            base.ActivateOptions();
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            using var ctw = new CsvTextWriter(writer);
            ctw.WriteQuote();
            base.Format(ctw, loggingEvent);
        }
    }
}
