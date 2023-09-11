namespace Helper.Payments.Core.Models.Invoices.ValueObjects
{
    public sealed record Street
    {
        public string Value { get; }

        public Street(string value)
        {
            Value = value;
        }
        public static implicit operator string(Street email) => email.Value;

        public static implicit operator Street(string email) => new(email);

        public override string ToString() => Value;
    }
}
