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
    public class RabbitMQIntegration : IMessageBrokerClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connectionConsume;
        private readonly IConnection _connectionPublish;

        public RabbitMQIntegration(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionConsume = CreateConnection(true);
            _connectionPublish = CreateConnection(false);
        }

        public IConnection CreateConnection(bool isForConsume)
        {

            if (!isForConsume)
            {
                IConnectionFactory factory = new ConnectionFactory()
                {
                    Uri = new Uri(_configuration.GetValue<string>("QueueHostName")),

                };
                return factory.CreateConnection();
            }
            else
            {
                IConnectionFactory factory = new ConnectionFactory()
                {
                    Uri = new Uri(_configuration.GetValue<string>("QueueHostName")),
                    DispatchConsumersAsync = true
                };
                return factory.CreateConnection();
            }
        }

        public async Task Publish(string @event)
        {
           
            using (var channel = _connectionPublish.CreateModel())
            {
                channel.QueueDeclare(queue: _configuration.GetValue<string>("QueuePayment"), // jak nie ma kolejki o takiej nazwie to się stworzy nowa
                    durable: false, // kolejka przetrwa kiedy zrestartujemy serwis
                    exclusive: false, // kiedy połączenie zotanie przerwane wszystkie wiadomości się kasują
                    autoDelete: false, // jak nie ma konsumentów to wiadomości się kasują
                    arguments: null);
                var body = Encoding.UTF8.GetBytes(@event); // kodujemy na tablicę bajtow
                channel.BasicPublish(exchange: "",
                            routingKey: _configuration.GetValue<string>("QueuePayment"),
                            basicProperties: null, body); // wysyłamy do kolejki
                
            }
        }

        public async Task ConsumeMessage(IServiceScope serviceScope)
        {
            var service = serviceScope.ServiceProvider.GetService<IMessageBrokerClient>();



            using (var _channel = _connectionConsume.CreateModel())
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
                            await HandleOfferAccepted(serviceScope, AcceptedOffer);
                        }
                        //tu mozna różne ify dla różnych obiektów
                    }
                };

                _channel.BasicConsume(queue: _configuration.GetValue<string>("QueueInvoice"), autoAck: true, consumer: consumer);
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
    }
}
