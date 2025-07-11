using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pagos.Application.Commands;
using Pagos.Application.Queries;
using Pagos.Application.servicios;

namespace Pagos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PagosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPago([FromBody] CrearPagoCommand command)
        {
            var pagoId = await _mediator.Send(command);
            return Ok(new { PagoId = pagoId });
        }

        [HttpPost("{id}/stripe/checkout")]
        public async Task<IActionResult> IniciarPagoStripe(
            Guid id,
            [FromBody] StripeCheckoutRequest request)
        {
            var command = new IniciarPagoStripeCommand
            {
                PagoId = id,
                SuccessUrl = request.SuccessUrl,
                CancelUrl = request.CancelUrl
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPago(Guid id)
        {
            var query = new ConsultarPagoPorIdQuery(id);
            var pago = await _mediator.Send(query);
            return pago != null ? Ok(pago) : NotFound();
        }
    }

    public class StripeCheckoutRequest
    {
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}