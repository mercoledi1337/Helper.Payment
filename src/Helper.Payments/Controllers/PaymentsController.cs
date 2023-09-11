using Helper.Payments.Core.Integrations;
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
        private readonly IRabbitMQIntegration _rabbitMQIntegration;

        public PaymentsController(IOfferService offerService
            ,IInvoiceService invoiceService
            ,IRabbitMQIntegration rabbitMQIntegration)
        {
            _offerService = offerService;
            _invoiceService = invoiceService;
            _rabbitMQIntegration = rabbitMQIntegration;
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

            await _rabbitMQIntegration.Publish(userId.ToString());
            return Ok(userId);
        }
    }
}
