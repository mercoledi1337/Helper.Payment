using Helper.Payments.Core.Integrations;

namespace Helper.Payments.Api
{
    public class PaymentBackgroudService : BackgroundService
    {
        private readonly IServiceProvider _service;

        public PaymentBackgroudService(IServiceProvider service)
        {
            _service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _service.CreateScope();
            var service = scope.ServiceProvider.GetService<IMessageBrokerClient>();

            while (true)
            {
                await service.ConsumeMessage(scope);
                await Task.Delay(1000);
            }
        }
    }
}
