using Microsoft.Extensions.DependencyInjection;

namespace Helper.Payments.Core.Integrations
{
    public interface IMessageBrokerClient
    {
        public Task Publish(string @event);
        public Task ConsumeMessage(IServiceScope serviceScope);

    }
}
