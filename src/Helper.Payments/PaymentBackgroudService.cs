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
            

            while (true)
            {
                var service = scope.ServiceProvider.GetService<IMessageBrokerClient>();
                await service.ConsumeMessage(scope);
                await Task.Delay(1000);
            }
        }
    }
}
