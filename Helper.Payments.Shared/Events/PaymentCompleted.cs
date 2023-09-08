
namespace Helper.Payments.Shared.Events
{
    public record PaymentCompleted(Guid PaymentId) : IEvent;
}
