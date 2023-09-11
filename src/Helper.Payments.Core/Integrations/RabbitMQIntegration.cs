using Helper.Payments.Core.Abstractions;
using Helper.Payments.Core.Models.Invoices;
using Helper.Payments.Shared.DTO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Helper.Payments.Core.Integrations
{
    public class RabbitMQIntegration : IRabbitMQIntegration
    {

        public async Task Publish(string InvoiceProForma)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" }; // adres do serwera rabbitMQ // tu zmienną potem
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Payment", // jak nie ma kolejki o takiej nazwie to się stworzy nowa
                    durable: false, // kolejka przetrwa kiedy zrestartujemy serwis
                    exclusive: false, // kiedy połączenie zotanie przerwane wszystkie wiadomości się kasują
                    autoDelete: false, // jak nie ma konsumentów to wiadomości się kasują
                    arguments: null);
                var body = Encoding.UTF8.GetBytes(InvoiceProForma); // kodujemy na tablicę bajtow
                channel.BasicPublish(exchange: "",
                            routingKey: "Payment",
                            basicProperties: null, body); // wysyłamy do kolejki
            }
        }

        public async Task ConsumeMEssage(IServiceScope serviceScope)
        {
            var service = serviceScope.ServiceProvider.GetService<IRabbitMQIntegration>();
            var serviceInvoice = serviceScope.ServiceProvider.GetService<ICommandHandler<InvoiceAccepted>>();
            var _factory = new ConnectionFactory()
            { HostName = "localhost", DispatchConsumersAsync = true };
            var _connection = _factory.CreateConnection();
            while (true)
            {
                using (var _channel = _connection.CreateModel())
                {
                    _channel.QueueDeclare(queue: "Invoice",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    var consumer = new AsyncEventingBasicConsumer(_channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var id = Encoding.UTF8.GetString(ea.Body.ToArray());
                        if (id != null)
                        {
                            var AcceptedOffer = JsonConvert.DeserializeObject<OfferacceptedEvent>(id);
                            if (AcceptedOffer is OfferacceptedEvent)
                            {
                                var InvoiceProForma = new InvoiceAccepted(AcceptedOffer);
                                await serviceInvoice.HandleAsync(InvoiceProForma);
                            }
                            //tu mozna różne ify dla różnych obiektów
                        }
                    };

                    _channel.BasicConsume(queue: "Invoice", autoAck: true, consumer: consumer);
                    await Task.Delay(1);
                }
                await Task.Delay(100);
            }
        }
    }
}
