using Helper.Payments.Core.Models.Invoices.ValueObjects;
using Helper.Payments.Shared.DTO;

namespace Helper.Payments.Core.Models.Invoices
{
    public class Invoice
    {
        public Guid InvoiceId { get; }
        public double Price { get; set; }
        public string Fullname { get; set; }
        public string Email { get;  set; }
        public string Street { get; set; }
        public int BankAccountNumber { get;set; }
        public DateTime RealisationStart { get; set; }
        public DateTime? RealisationEnd { get; set; }

        private Invoice(Guid id, OfferacceptedEvent invoiceDto, string street, int bankAccountNumber)
        {
            InvoiceId = id;
            Email = invoiceDto.Email;
            Price = invoiceDto.Price;
            Fullname = invoiceDto.Fullname;
            Street = street;
            BankAccountNumber = bankAccountNumber;
            RealisationEnd = invoiceDto.RealisationEnd;
            RealisationStart = invoiceDto.RealisationStart;
        }

        public static Invoice Create(OfferacceptedEvent invoiceDto, string street, int bankAccountNumber)
        {
            Guid id = invoiceDto.OfferId;
            return new Invoice(id, invoiceDto, street, bankAccountNumber);
        }

        private Invoice()
        {

        }
    }
}
