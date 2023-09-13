using Helper.Payments.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Payments.Api.Controllers
{
    [ApiController]
    [Route("Payments")]
    public class PaymentsController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public PaymentsController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        //[HttpPost]
        //public async Task<IActionResult> PostInvoice(Guid offer)
        //{
        //    //_invoiceService.Skrt(offer).Result
        //    return Ok();
        //}
        [HttpPost("Payment")]
        public async Task<IActionResult> InvoicePayment(Guid InvoiceId,Guid userId)
        {
            await _invoiceService.PayTheInvoice(InvoiceId,userId);
            return Ok(userId);
        }

        [HttpGet("UeserInvoices")]
        public async Task<IActionResult> GetAll(string mail)
        {
            var Invoices = await _invoiceService.GetAllUserInvoiceByMail(mail);
            return Ok(Invoices);
        }
    }
}
