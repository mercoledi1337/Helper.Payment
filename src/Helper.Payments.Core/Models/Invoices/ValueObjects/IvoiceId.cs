namespace Helper.Payments.Core.Models.Invoices.ValueObjects
{
    public record InvoiceId
    {
        public Guid Value { get; }

        public InvoiceId(Guid value)
        {
            Value = value;
        }

        public static InvoiceId Create() => new(Guid.NewGuid());

        public static implicit operator Guid(InvoiceId date) => date.Value;
        public static implicit operator InvoiceId(Guid value) => new(value);
    }
}
