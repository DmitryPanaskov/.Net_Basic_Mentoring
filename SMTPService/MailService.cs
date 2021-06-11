namespace SMTPService
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class MailService : IMailService
    {
        public void SentToMail(string message, string subject = null)
        {
            var fromAddress = new MailAddress("mentoring_2021@mail.com");
            var toAddress = new MailAddress("mantoring_2021@mail.com");
            const string fromPassword = "4TRb91VELOrtnCPDmFLD";

            var smtp = new SmtpClient
            {
                Host = "smtp.mail.ru",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            try
            {
                smtp.Send(new MailMessage(fromAddress, toAddress) { Subject = subject, Body = message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


        public void SentToGmail(string message, string subject = null)
        {
            var fromAddress = new MailAddress("");
            var toAddress = new MailAddress("");
            const string fromPassword = "***";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            try
            {
                smtp.Send(new MailMessage(fromAddress, toAddress) { Subject = subject, Body = message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
