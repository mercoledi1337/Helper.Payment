using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Helper.Payments.Core.Integrations
{
    public class MailClient : IMailClient
    {
        private readonly IConfiguration _configuration;

        public MailClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public async Task SendMail(string addressTo, string subject, string body)
        //{
        //    string from = "bigboss1337skrt@gmail.com";
        //    //schować hasło

        //    MimeMessage message = new MimeMessage();
        //    message.From.Add(MailboxAddress.Parse("bigboss1337skrt@gmail.com"));
        //    message.To.Add(MailboxAddress.Parse(addressTo));
        //    message.Subject = subject;
        //    message.Body = new TextPart(TextFormat.Html) { Text = body };


        //    using (SmtpClient client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
        //        client.Authenticate("bigboss1337skrt@gmail.com", _configuration.GetValue<string>("password"));

        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}

        public async Task SendMailWithPdf(string addressTo, string subject, string body, byte[] pdf)
        {
            string from = "bigboss1337skrt@gmail.com";
            //schować hasło

            var attachment = new Attachment(new MemoryStream(pdf), "invoice.pdf", "application/pdf");

            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(addressTo));
            message.Subject = "test";
            message.Body = "test";
            message.From = new MailAddress(from);
            message.IsBodyHtml = false;
            Attachment data = attachment;
            message.Attachments.Add(data);


            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("bigboss1337skrt@gmail.com", _configuration.GetValue<string>("password"))
            };
                try
            {
                client.Send(message);
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
            }
        }
    }

