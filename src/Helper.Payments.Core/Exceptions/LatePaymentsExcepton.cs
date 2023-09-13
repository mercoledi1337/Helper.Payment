using Helper.Core.Exceptions;

namespace Helper.Payments.Core.Exceptions
{
    public class LatePaymentException : CustomException
    {
        public LatePaymentException() : base("Late Payment")
        {
        }
    }
}
