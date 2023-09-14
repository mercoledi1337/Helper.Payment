using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Helper.Payments.Core.Integrations
{
    public interface IMessageBrokerClient
    {
        public Task Publish(string @event);
        public Task ConsumeMessage(IServiceScope serviceScope);
        public IConnection CreateConnection(bool isConsuming);
    }
}
