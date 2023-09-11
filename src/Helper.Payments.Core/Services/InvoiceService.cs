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
            var tmp = Invoice.Create(dto, "skrt", new BankAccountNumber(999) );
            return tmp;
        }

        public async Task AddInvoiceProForma(Invoice invoice)
        {
            await _paymentDbContext.Invoices.AddAsync(invoice);
            await _paymentDbContext.SaveChangesAsync();
        }

        //public async Task<List<Guid>> Skrt(Guid OfferId)
        //{
        //    FormattableString q = $"SELECT * FROM api.Offers WHERE Offerid = {OfferId}";
        //    var blogNames = _paymentDbContext.Database.SqlQuery <Guid >(q).ToList();
        //    return blogNames;
        //}
    }
}
