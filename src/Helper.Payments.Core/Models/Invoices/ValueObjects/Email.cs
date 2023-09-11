namespace Helper.Payments.Core.Models.Invoices.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            Value = value;
        }
        public static implicit operator string(Email email) => email.Value;

        public static implicit operator Email(string email) => new(email);

        public override string ToString() => Value;
    }
}
