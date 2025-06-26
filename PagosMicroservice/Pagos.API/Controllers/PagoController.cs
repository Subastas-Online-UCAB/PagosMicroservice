using Microsoft.AspNetCore.Mvc;
using Pagos.Application.DTOs;
using Pagos.Application.Servicios;

namespace Pagos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagoController : ControllerBase
{
	private readonly IPagoservicio _pagoServicio;

	public PagoController(IPaymentServicio pagoServicio)
	{
		_pagoServicio = pagoServicio;
	}

	[HttpPost]
	public async Task<IActionResult> ProcessPayment([FromBody] CrearPaymentDto pagoDto)
	{
		var result = await _pagoServicio.ProcessPaymentAsync(pagoDto);
		return Ok(result);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetPayment(Guid id)
	{
		var payment = await _pagoServicio.GetPaymentByIdAsync(id);
		if (payment == null) return NotFound();
		return Ok(payment);
	}

	[HttpGet("user/{IdUsuario}")]
	public async Task<IActionResult> GetPaymentsByUser(string IdUsuario)
	{
		var payments = await _pagoServicio.GetPaymentsByIdUsuarioAsync(IdUsuario);
		return Ok(payments);
	}

	[HttpGet("auction/{auctionId}")]
	public async Task<IActionResult> GetPaymentsByAuction(string IdSubasta)
	{
		var payments = await _pagoServicio.GetPaymentsByIdSubastaAsync(IdSubasta);
		return Ok(payments);
	}
}