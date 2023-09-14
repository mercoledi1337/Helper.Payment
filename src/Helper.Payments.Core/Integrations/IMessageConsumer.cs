using Microsoft.Extensions.DependencyInjection;

namespace Helper.Payments.Core.Integrations
{
    public interface IMessageConsumer
    {
        public Task ConsumeMessage(IServiceScope serviceProvider);
    }
}
