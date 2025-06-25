namespace Pagos.Application.DTOs;

public class CrearPagoDto
{
	public string IdUsuario { get; set; }
	public string IdSubasta { get; set; }
	public decimal Cantidad { get; set; };
}