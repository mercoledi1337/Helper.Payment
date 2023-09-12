using Helper.Payments.Core.Data;
using Helper.Payments.Core.Models.Invoices;
using Helper.Payments.Core.Models.Invoices.ValueObjects;
using Helper.Payments.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace Helper.Payments.Core.Services
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly PaymentDbContext _paymentDbContext;

        public InvoiceService(PaymentDbContext paymentDbContext)
        {
            _paymentDbContext = paymentDbContext;
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
    }
}
