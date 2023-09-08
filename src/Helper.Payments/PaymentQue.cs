using Helper.Payments.Core.Abstractions;
using Helper.Payments.Core.Models.Invoices;
using Helper.Payments.Shared.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Helper.Payments.Api
{
    public class PaymentQue : BackgroundService
    {
        private readonly IServiceProvider _service;

        private IConnection _connection;

        public PaymentQue(IServiceProvider service)
        {
            var _factory = new ConnectionFactory()
            { HostName = "localhost", DispatchConsumersAsync = true };
            _connection = _factory.CreateConnection();
            _service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _service.CreateScope();
            var service = scope.ServiceProvider.GetService<ICommandHandler<InvoiceAccepted>>();
            
            while (true)
            {
                using (var _channel = _connection.CreateModel())
                {
                    _channel.QueueDeclare(queue: "Invoice",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null); // tutaj wszystko tak samo jak u producenta
                    var consumer = new AsyncEventingBasicConsumer(_channel);
                    consumer.Received += async (model, ea) =>
                    {
                        //refactor na komponenty
                        //
                        var id = Encoding.UTF8.GetString(ea.Body.ToArray());
                        if (id != null)
                        {
                            var tmp = JsonConvert.DeserializeObject<OfferacceptedEvent>(id);
                            var tmp1 = new InvoiceAccepted(tmp);
                            await service.HandleAsync(tmp1);
                        }
                       
                        // event tutaj
                    };
                    
                    _channel.BasicConsume(queue: "Invoice", autoAck: true, consumer: consumer);
                    await Task.Delay(1);
                    //shared z event takim samym w app
                    //w drugiej solucji to samo

                    //autoAck czy napewno wiadomośc została obsłużona i może zostać usunięta z kolejki
                }

                await Task.Delay(100);
            }
        }
    }
}
