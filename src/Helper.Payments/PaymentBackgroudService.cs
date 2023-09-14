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
                
                try { await service.ConsumeMessage(scope); }
                catch (Exception ex) { Console.WriteLine(ex); }
                await Task.Delay(100);
            }
        }
    }
}
