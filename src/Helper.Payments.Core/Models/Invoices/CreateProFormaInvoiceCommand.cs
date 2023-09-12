using Helper.Payments.Core.Abstractions;
using Helper.Payments.Shared.DTO;

namespace Helper.Payments.Core.Models.Invoices
{
    public sealed record CreateProFormaInvoiceCommand(OfferacceptedEvent Dto) : ICommand;

}
