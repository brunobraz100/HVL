using System.Threading.Tasks;
using hvl.services.interfaces;
using System;
//using System.Net.Mail;
//using System.Net;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace hvl.services
{
    public class EmailServices : IEmailSender
    {
        public async Task<bool> SendMail(string to, string subject, string message)
        {
            bool sended = await SendMailAsync(to: to, subject: subject, message: message);
            return sended;
        }

        public async Task<bool> SendMailAsync(string to, string subject, string message)
        {
            bool sended = true;
            try
            {
                // MailMessage mail = new MailMessage();
                // mail.From = new MailAddress(address: Settings.Email, displayName: "Não responda");
                // mail.To.Add(new MailAddress(to));
                // mail.Subject = subject;
                // mail.Body = message;
                // mail.IsBodyHtml = true;
                // mail.Priority = MailPriority.High;

                // using(SmtpClient client = new SmtpClient(Settings.SMTP, Settings.Porta))
                // {
                //     client.UseDefaultCredentials = false;
                //     client.Credentials = new NetworkCredential(Settings.Email, Settings.Senha);
                //     client.EnableSsl = true;
                //     client.Send(mail);
                // }

                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse(Settings.Email));
                mail.To.Add(MailboxAddress.Parse(to));
                mail.Subject = subject;
                mail.Body = new TextPart(TextFormat.Html) {Text = message};

                using(SmtpClient client = new SmtpClient())
                {   
                    client.Connect(Settings.SMTP, Settings.Porta, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(Settings.Email, Settings.Senha);
                    client.Send(mail);
                    client.Disconnect(true);
                }


            } catch(Exception ex)
            {
                sended = false;
            }

            return sended;
        }

    }
}