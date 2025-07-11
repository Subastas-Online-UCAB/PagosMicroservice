using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pagos.Application.Commands;
using Pagos.Application.Queries;
using Pagos.Application.servicios;

namespace Pagos.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PagosControlador : ControllerBase
    {
        private readonly IMediator _mediator;

        public PagosControlador(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea pago.
        [HttpPost]
        public async Task<IActionResult> CrearPago([FromForm] CrearPagoCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }


        /// <summary>

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(new { Id = id, Mensaje = "Pago recuperado (placeholder)" });
        }



        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resultado = await _mediator.Send(new GetAllPagosQuery());
            return Ok(resultado);
        }



        [HttpPut("actualizar")]
        public async Task<IActionResult> ActualizarPago(Guid id, [FromForm] ActualizarPagoCommand command)
        {
            command.PagoId = id; // Asignar el ID desde la ruta
            var resultado = await _mediator.Send(command);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }



        /// <summary>
        /// Consulta un pago por su ID.
        [HttpGet("buscar/{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(new ConsultarPagoPorIdQuery(id), cancellationToken);

            if (resultado is null)
                return NotFound();

            return Ok(resultado);
        }

    }
}
