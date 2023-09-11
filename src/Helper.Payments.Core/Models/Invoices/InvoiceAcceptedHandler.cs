using Helper.Payments.Core.Abstractions;
using Helper.Payments.Core.Services;

namespace Helper.Payments.Core.Models.Invoices
{
    public class InvoiceAcceptedHandler : ICommandHandler<InvoiceAccepted>
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceAcceptedHandler(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        public async Task HandleAsync(InvoiceAccepted command)
        {
            var invoice = await _invoiceService.MakeInvoice(command.Dto);
            _invoiceService.AddInvoiceProForma(invoice);
        }
    }
}

