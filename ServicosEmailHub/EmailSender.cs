using System.Net.Mail;
using System.Text;

namespace ServicosEmailHub
{
    public class EmailSender : IEmailSender
    {
        private SmtpClient _client;
        public EmailSender(EmailConfiguration configuration)
        {
            string host = configuration.Host;
            int port = configuration.Port;
            string smtpUserName = configuration.SmtpUserName;
            string smtpPassword = configuration.SmtpPassword;

            _client = new SmtpClient(host, port);
            _client.UseDefaultCredentials = false;
            _client.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
            _client.EnableSsl = true;
        }

        public void SendEmail(MensagemEmail message)
        {
            MailMessage msg = criaMensagem(message);
            _client.Send(msg);
        }


        private MailMessage criaMensagem(MensagemEmail message)
        {
            MailAddress fromAddress = new MailAddress(message.From, message.Displayname, Encoding.UTF8);
            MailAddress toAddress = new MailAddress(message.To, message.Displayname, Encoding.UTF8);
            MailMessage msg = new MailMessage(fromAddress, toAddress);
            msg.Body = message.Content;
            msg.BodyEncoding = Encoding.UTF8;
            msg.Subject = message.Subject;
            msg.SubjectEncoding = Encoding.UTF8;

            return msg;
        }
    }
}
