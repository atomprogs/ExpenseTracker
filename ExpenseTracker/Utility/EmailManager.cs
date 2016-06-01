using ExpenseTracker.Models;
using ExpenseTracker.Utility.Enumeration;
using System;
using System.IO;
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
        public static void SendMail(Users User, MailType EmailType)
        {
            try
            {
                MailMessage mailMsg = new MailMessage();
                string[] toAddrs = User.Email.Split(';');
                // To
                foreach (string addr in toAddrs)
                {
                    mailMsg.To.Add(new MailAddress(addr));
                }

                // From
                mailMsg.From = new MailAddress("expensetracker@trackyourexpense.azurewebsites.net", "Expense Tracker");

                // Subject and multipart/alternative Body
                mailMsg.Subject = EmailType == MailType.SignUpCode ? "SignUp code" : EmailType == MailType.SignUp ? "Welcome to ExpenseTracker" : "Notification";
                string html = string.Empty;
                switch (EmailType)
                {
                    case MailType.SignUp:
                        if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates//Email//SignUp.html")))
                        {
                            html = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates//Email//SignUp.html"));
                            html = html.Replace("[UserId]", User.Email).Replace("[Password]", User.Password);
                        }
                        break;

                    case MailType.SignUpCode:
                        if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates//Email//SignUpCode.html")))
                        {
                            html = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates//Email//SignUpCode.html"));
                            html = html.Replace("[code]", Convert.ToString(User.SignUpCode));
                        }
                        break;

                    case MailType.Notification:
                        if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates//Email//SignUp.html")))
                        {
                            html = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates//Email//SignUp.html"));
                        }
                        break;
                }
                // mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
                mailMsg.IsBodyHtml = true;
                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("azure_62c9778703dadb1f7e3f5e08c698c7d1@azure.com", "0Z9C0nk62DxUFNe");
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = false;

                smtpClient.Send(mailMsg);
            }
            catch
            {
                throw;
            }
        }
    }
}