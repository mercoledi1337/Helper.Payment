using Helper.Payments.Core.Services;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace Helper.Payments.Api.Controllers
{
    [ApiController]
    [Route("Payments")]
    public class PaymentsController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly IInvoiceService _invoiceService;

        public PaymentsController(IOfferService offerService, IInvoiceService invoiceService)
        {
            _offerService = offerService;
            _invoiceService = invoiceService;
        }
        [HttpPost]
        public async Task<IActionResult> PostInvoice(Guid offer)
        {
            //_invoiceService.Skrt(offer).Result
            return Ok();
        }
        [HttpPost("Payment")]
        public async Task<IActionResult> InvoicePayment(Guid userId)
        {
            
            var factory = new ConnectionFactory() { HostName = "localhost" }; // adres do serwera rabbitMQ
            using (var connection = factory.CreateConnection()) // połącznie
            using (var channel = connection.CreateModel()) 
            {
                channel.QueueDeclare(queue: "Payment", // jak nie ma kolejki o takiej nazwie to się stworzy nowa
                    durable: false, // kolejka przetrwa kiedy zrestartujemy serwis
                    exclusive: false, // kiedy połączenie zotanie przerwane wszystkie wiadomości się kasują
                    autoDelete: false, // jak nie ma konsumentów to wiadomości się kasują
                    arguments: null);
                var body = Encoding.UTF8.GetBytes(userId.ToString()); // kodujemy na tablicę bajtow
                channel.BasicPublish(exchange: "",
                    routingKey: "Payment",
                    basicProperties: null, body); // wysyłamy do kolejki
            }
            return Ok(userId);
        }
    }
}
