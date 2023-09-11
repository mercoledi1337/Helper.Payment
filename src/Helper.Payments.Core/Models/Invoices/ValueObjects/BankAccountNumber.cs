namespace Helper.Payments.Core.Models.Invoices.ValueObjects
{
    public sealed record BankAccountNumber
    {
        public int Value { get; }

        public BankAccountNumber(int value)
        {

            Value = value;
        }

        public static implicit operator int(BankAccountNumber date) => date.Value;
        public static implicit operator BankAccountNumber(int value) => new(value);
    }
}
