namespace Helper.Payments.Core.Models.Invoices.ValueObjects
{
    public sealed record Price
    {
        public double Value { get; }

        public Price(double value)
        {

            Value = value;
        }

        public static implicit operator double(Price date) => date.Value;
        public static implicit operator Price(double value) => new(value);
    }
}
