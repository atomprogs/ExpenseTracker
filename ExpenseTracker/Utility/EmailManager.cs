using System;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;

namespace AutoRecoveryServices.Email
{
    public static class EmailManager
    {
        /// <summary>
        /// Sends the mail.
        /// </summary>
        public static void SendMail()
        {
            try
            {
                MailMessage mailMsg = new MailMessage();
                string[] toAddrs = null;

                toAddrs = new string[] { "rajeev.r@advanced-india.com" };

                // To
                foreach (string addr in toAddrs)
                {
                    mailMsg.To.Add(new MailAddress(addr));
                }

                // From
                mailMsg.From = new MailAddress("", "");

                // Subject and multipart/alternative Body
                mailMsg.Subject = Resource_en.EmailSubject;
                string html = string.Empty;

                // mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
                mailMsg.IsBodyHtml = true;
                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient(config.SmtpServer, Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(config.SmtpUsername, config.SmtpPassword);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = config.EnableSSL;

                smtpClient.Send(mailMsg);
            }
            catch
            {
                throw;
            }
        }
    }
}