

namespace Helper.Payments.Core.Integrations
{
    public interface IMailClient
    {
        //Task SendMail(string to, string subject, string body);
        public Task SendMailWithPdf(string addressTo, string subject, string body, byte[] pdf);
    }
}
