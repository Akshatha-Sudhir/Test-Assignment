using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using NUnit.Framework;
using System.Configuration;
using System.Windows.Forms;


namespace TestAssignment.TestCases
{
    class SendEmail
    {
        [Test]
        public void send_email()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(ConfigurationManager.AppSettings["mailfrom"]);
                mail.To.Add(ConfigurationManager.AppSettings["mailto"]);
                mail.Subject = "Test Reports";
                mail.Body = "Find the Test reports in the attachment";

                System.Net.Mail.Attachment attachment_OnlyNumbers,attachment_Phone,attachment_ZeroCents;
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string reportPath_OnlyNumbers = projectPath + "Reports\\OnlyNumbers.html";
                string reportPath_Phone = projectPath + "Reports\\Phone.html";
                string reportPath_ZeroCents = projectPath + "Reports\\ZeroCents.html";
                attachment_OnlyNumbers = new System.Net.Mail.Attachment(reportPath_OnlyNumbers);
                attachment_Phone = new System.Net.Mail.Attachment(reportPath_OnlyNumbers);
                attachment_ZeroCents = new System.Net.Mail.Attachment(reportPath_OnlyNumbers);
                mail.Attachments.Add(attachment_OnlyNumbers);
                mail.Attachments.Add(attachment_Phone);
                mail.Attachments.Add(attachment_ZeroCents);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("automationtestuser08", "testuser08");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
