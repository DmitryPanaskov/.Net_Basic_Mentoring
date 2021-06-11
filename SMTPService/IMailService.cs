namespace SMTPService
{
    using System.Threading.Tasks;

    public interface IMailService
    {
        public void SentToMail(string message, string subject = null);

        public void SentToGmail(string message, string subject = null);
    }
}
