using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Helper.Payments.Core.Integrations
{
    public interface IRabbitMQIntegration
    {
        public Task Publish(string CatalogInfo);
        public Task ConsumeMEssage(IServiceScope serviceScope);

    }
}
