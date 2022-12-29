using System;
using System.Configuration;
using System.Net.Mail;

namespace Sigv.Web.App
{
    public class Mailer
    {
        // Parametros
        public static string MailSenderMessage { get; set; }
        public static string SiteName { get; set; }
        public static string MailPort { get; set; }
        public static string MailSmtp { get; set; }
        public static string MailConnect { get; set; }
        public static string MailConnectPwd { get; set; }

        // Enviar Email
        public static bool Send(string mailTo, string replayTo, string mailSubject, string message, string pathAnexos, string[] anexos)
        {
            try
            {
                // Carrega as configurações
                MailSenderMessage = ConfigurationManager.AppSettings["MailSenderMessage"];
                SiteName = ConfigurationManager.AppSettings["SiteName"];
                MailPort = ConfigurationManager.AppSettings["MailPort"];
                MailSmtp = ConfigurationManager.AppSettings["MailSmtp"];
                MailConnect = ConfigurationManager.AppSettings["MailConnect"];
                MailConnectPwd = ConfigurationManager.AppSettings["MailConnectPwd"];

                var mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(MailSenderMessage, SiteName);

                mailMessage.To.Add(new MailAddress(mailTo));
                mailMessage.CC.Add(new MailAddress(replayTo));
                mailMessage.Subject = mailSubject;
                mailMessage.Body = message;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.IsBodyHtml = true;

                // Se tiver anexos
                foreach (var anexo in anexos)
                {
                    if (anexo == null) continue;
                    var attachment = new Attachment(pathAnexos + "\\" + anexo);
                    mailMessage.Attachments.Add(attachment);
                }

                var mailClient = new SmtpClient
                {
                    Port = int.Parse(MailPort),
                    Host = MailSmtp,
                    EnableSsl = true,
                    UseDefaultCredentials = true,
                    Credentials = new System.Net.NetworkCredential(MailConnect, MailConnectPwd)
                };

                mailClient.Send(mailMessage);
                mailClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}