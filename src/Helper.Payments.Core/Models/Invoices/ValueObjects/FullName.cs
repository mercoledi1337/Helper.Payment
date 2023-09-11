namespace Helper.Payments.Core.Models.Invoices.ValueObjects
{
    public sealed record FullName
    {
        public string Value { get; }

        public FullName(string value)
        {
            Value = value;
        }

        public static implicit operator FullName(string value) => value is null ? null : new FullName(value);

        public static implicit operator string(FullName value) => value?.Value;

        public override string ToString() => Value;
    }
}
