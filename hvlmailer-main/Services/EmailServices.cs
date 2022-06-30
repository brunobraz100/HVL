using System;
using System.Threading.Tasks;
using hvlMailer.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace hvlMailer.Services
{
    public class EmailServices
    {
        private readonly string Subject = "Aviso de Vencimento";
        private string message = "";
        public async Task<bool> sendMail(AgendaVencimentoModel vencimento)
        {
            bool sended = true;

            message = $"Sua AET {vencimento.DsAET}, do veículo "
            + $"{vencimento.DsPlaca}, do Estado de {vencimento.DsUF}, vencerá no dia "+
            $"{vencimento.DtVencimento.ToString("dd/MM/yyyy")}. "+
            "\nPor favor, entre em contato conosco para realizarmos sua renovação. "+
            "\n\nNossos canais de atendimento são: \n\n- Licenças estaduais: \ntel: (11) 4077-1030 / - whatsapp: (11) 99100-1002, \nE-mail: hvlservicos@uol.com.br \n\n" +
            "- Licenças DNIT: \nwhatsapp (67) 99256-6463 \nE-mail: hvlservicos3@uol.com.br \n\n" +  
            "Obrigado, \nHVL Serviços.";

            try
            {
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse(Settings.Email));
                mail.To.Add(MailboxAddress.Parse(vencimento.DsEmail));
                mail.Subject = Subject;
                mail.Body = new TextPart(TextFormat.Text) {Text = message};

                using(SmtpClient client = new SmtpClient())
                {   
                    client.Connect(Settings.SMTP, Settings.Port, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(Settings.Email, Settings.Password);
                    client.Send(mail);
                    client.Disconnect(true);
                }
            }catch(Exception ex)
            {
                Console.WriteLine($"Erro ao tentar enviar e-mail com aviso de vencimento para ${vencimento.DsCliente}.");
                sended = false;
            }


            return sended;
        }

        public async Task<bool> sendReport(int enviados)
        {
            bool sended = true;

            message = $"Olá, \n Rotina disparou {enviados} e-mails. \n\n Obrigado.";

            try
            {
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse(Settings.Email));
                mail.To.Add(MailboxAddress.Parse("brservicosinf@gmail.com"));
                mail.Subject = Subject;
                mail.Body = new TextPart(TextFormat.Text) {Text = message};

                using(SmtpClient client = new SmtpClient())
                {   
                    client.Connect(Settings.SMTP, Settings.Port, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(Settings.Email, Settings.Password);
                    client.Send(mail);
                    client.Disconnect(true);
                }
            }catch(Exception ex)
            {
                Console.WriteLine($"Erro ao tentar enviar e-mail reportando quantidade de envios.");
                sended = false;
            }


            return sended;
        }


    }
}