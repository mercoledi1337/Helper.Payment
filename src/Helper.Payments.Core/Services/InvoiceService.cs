using Helper.Payments.Core.Data;
using Helper.Payments.Core.Exceptions;
using Helper.Payments.Core.Integrations;
using Helper.Payments.Core.Models.Invoices;
using Helper.Payments.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace Helper.Payments.Core.Services
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly PaymentDbContext _paymentDbContext;
        private readonly IMessagePublisher _messageBrokerClient;
        private readonly IMailClient _mailClient;
        private readonly IPdfGenerator _pdfGenerator;

        public InvoiceService(PaymentDbContext paymentDbContext
            , IMessagePublisher messageBrokerClient
            , IMailClient mailClient
            , IPdfGenerator pdfGenerator)
        {
            _paymentDbContext = paymentDbContext;
            _messageBrokerClient = messageBrokerClient;
            _mailClient = mailClient;
            _pdfGenerator = pdfGenerator;
        }

        public async Task<Invoice> MakeInvoice(OfferacceptedEvent dto)
        {
            var tmp = Invoice.Create(dto, "skrt", 1000 );
            return tmp;
        }

        public async Task AddInvoiceProForma(Invoice invoice)
        {
            try
            {
                await _paymentDbContext.Invoices.AddAsync(invoice);
                await _paymentDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task PayTheInvoice(Guid InvoiceId, Guid UserId)
        {
            await Task.Delay(1000); //to po to że niby platność jest przetwarzana
            var Invoice = await _paymentDbContext.Invoices.FirstOrDefaultAsync(x => x.InvoiceId == InvoiceId);
            if (Invoice.PaymentDate >  DateTime.UtcNow) 
            {
                throw new LatePaymentException();
            }
            Invoice.IsPaid = true;
            await _paymentDbContext.SaveChangesAsync();//może tak zostać czy do invoicerepository zrobić?

             _messageBrokerClient.Publish(UserId.ToString());
            var document = _pdfGenerator.GenerateInvoice(Invoice);
            await _mailClient.SendMailWithPdf(Invoice.Email, "Invoice number:" + 3 +"", "Your invoice", document);
        }

        public async Task<List<Invoice>> GetAllUserInvoiceByMail(string email) => await _paymentDbContext.Invoices.Where(x => x.Email == email).ToListAsync();
    }
}
