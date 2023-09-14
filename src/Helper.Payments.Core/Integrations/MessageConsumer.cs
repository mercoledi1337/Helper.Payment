using Helper.Payments.Core.Services;
using Helper.Payments.Shared.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Helper.Payments.Core.Integrations
{
    public class MessageConsumer : IMessageConsumer
    {
        private IConnection _connection;
        private readonly IConfiguration _configuration;

        public MessageConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = Factory().CreateConnection();
        }

        public async Task ConsumeMessage(IServiceScope serviceProvider)
        {
            var service = serviceProvider.ServiceProvider.GetService<IMessageConsumer>();

            using (var _channel = _connection.CreateModel())
            {
                _channel.QueueDeclare(queue: _configuration.GetValue<string>("QueueInvoice"),
                   durable: false,
                   exclusive: false,
                   autoDelete: false,
                   arguments: null);

                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.Received += async (model, ea) =>
                {
                    var @event = Encoding.UTF8.GetString(ea.Body.ToArray());
                    if (@event != null)
                    {
                        var AcceptedOffer = JsonConvert.DeserializeObject<OfferacceptedEvent>(@event);
                        if (AcceptedOffer is OfferacceptedEvent)
                        {
                            await HandleOfferAccepted(serviceProvider, AcceptedOffer);
                        }
                    }
                };

                _channel.BasicConsume(queue: _configuration.GetValue<string>("QueueInvoice")
                    , autoAck: true, consumer: consumer);
                await Task.Delay(1);
            }
        }

        private static async Task HandleOfferAccepted(IServiceScope serviceScope, OfferacceptedEvent AcceptedOffer)
        {
            var serviceInvoice = serviceScope.ServiceProvider.GetService<IInvoiceService>();
            var InvoiceProForma = await serviceInvoice.MakeInvoice(AcceptedOffer);
            await serviceInvoice.AddInvoiceProForma(InvoiceProForma);
            await Task.CompletedTask;
        }

        private IConnectionFactory Factory()
        {
            return new ConnectionFactory()
            {
                Uri = new Uri(_configuration.GetValue<string>("QueueHostName")),
                DispatchConsumersAsync = true
            };
        }
    }
}
