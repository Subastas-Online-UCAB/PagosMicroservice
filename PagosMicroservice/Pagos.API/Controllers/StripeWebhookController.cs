using Microsoft.AspNetCore.Mvc;
using Pagos.Infrastructure.Stripe;
using System.IO;
using System.Threading.Tasks;

namespace Pagos.Api.Controllers
{
    [ApiController]
    [Route("api/webhooks/stripe")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly StripeWebhookService _webhookService;

        public StripeWebhookController(StripeWebhookService webhookService)
        {
            _webhookService = webhookService;
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook()
        {
            using var reader = new StreamReader(HttpContext.Request.Body);
            var json = await reader.ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"];

            await _webhookService.HandleWebhookAsync(json, signature);

            return Ok();
        }
    }
}