using Helper.Payments.Core.Models.Invoices.ValueObjects;
using Helper.Payments.Shared.DTO;

namespace Helper.Payments.Core.Models.Invoices
{
    public class Invoice
    {
        public Guid InvoiceId { get; }
        public double Price { get; private set; }
        public string Fullname { get; private set; }
        public string Email { get; private set; }
        public string Street { get; private set; }
        public int BankAccountNumber { get; private set; }

        private Invoice(InvoiceId id, OfferacceptedEvent invoiceDto, Street street, BankAccountNumber bankAccountNumber)
        {
            InvoiceId = id;
            Email = invoiceDto.Email;
            Price = invoiceDto.Price;
            Fullname = invoiceDto.Fullname;
            Street = street;
            BankAccountNumber = bankAccountNumber;
        }

        public static Invoice Create(OfferacceptedEvent invoiceDto, Street street, BankAccountNumber bankAccountNumber)
        {
            InvoiceId id = invoiceDto.OfferId;
            return new Invoice(id, invoiceDto, street, bankAccountNumber);
        }

        private Invoice()
        {

        }
    }
}
