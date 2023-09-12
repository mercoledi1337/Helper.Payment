namespace Helper.Payments.Core.Models.Invoices
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetByIdAsync(Guid InvoiceId);
        
    }
}
