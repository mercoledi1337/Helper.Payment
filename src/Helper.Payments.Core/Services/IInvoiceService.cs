using Helper.Payments.Core.Models.Invoices;
using Helper.Payments.Shared.DTO;

namespace Helper.Payments.Core.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> MakeInvoice(OfferacceptedEvent dto);
        Task AddInvoiceProForma(Invoice invoice);
        Task PayTheInvoice(Guid InvoiceId);
        Task<List<Invoice>> GetAllUserInvoiceByMail(string email);
        //opłać //utwórz fakture //Lista platności 
    }
}
