namespace Helper.Payments.Core.Integrations
{
    public interface IMessagePublisher
    {
        void Publish(string message);
    }
}
