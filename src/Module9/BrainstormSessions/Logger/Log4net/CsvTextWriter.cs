using System.IO;
using System.Text;

namespace BrainstormSessions.Logger.Log4net
{
    public class CsvTextWriter : TextWriter
    {
        private readonly TextWriter textWriter;

        public CsvTextWriter(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        public override Encoding Encoding => this.textWriter.Encoding;

        public override void Write(char value)
        {
            this.textWriter.Write(value);
            if (value == '"')
            {
                this.textWriter.Write(value);
            }
        }

        public void WriteQuote()
        {
            this.textWriter.Write('"');
        }
    }
}
