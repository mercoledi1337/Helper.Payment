

using Helper.Payments.Core.Models.Invoices;

namespace Helper.Payments.Core.Integrations
{
    public interface IPdfGenerator
    {
        public byte[] GenerateInvoice(Invoice invoice);
    }
}
