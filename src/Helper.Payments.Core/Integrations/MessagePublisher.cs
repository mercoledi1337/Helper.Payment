using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace Helper.Payments.Core.Integrations
{
    public class MessagePublisher : IMessagePublisher
    {
        private IConnection _connection;
        private readonly IConfiguration _configuration;

        public MessagePublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = Factory().CreateConnection();
        }

        public void Publish(string @event)
        {
            using (var channel = _connection.CreateModel())
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

        private IConnectionFactory Factory()
        {
            return new ConnectionFactory()
            {
                Uri = new Uri(_configuration.GetValue<string>("QueueHostName")),
            };
        }
    }
}
