using Helper.Payments.Core.Integrations;

namespace Helper.Payments.Api
{
    public class PaymentQue : BackgroundService
    {
        private readonly IServiceProvider _service;

        public PaymentQue(IServiceProvider service)
        {
            _service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _service.CreateScope();
            var service = scope.ServiceProvider.GetService<IRabbitMQIntegration>();
            await service.ConsumeMEssage(scope);
            //testgit
        }
    }
}
