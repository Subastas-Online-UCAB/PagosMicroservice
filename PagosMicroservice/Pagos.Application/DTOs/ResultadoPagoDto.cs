namespace Pagos.Application.DTOs;

public class ResultadoPagoDto
{
    public Guid Id { get; set; }
    public string IdUsuario { get; set; }
    public string IdSubasta { get; set; }
    public decimal Cantidad { get; set; }
    public string PaymentIntentId { get; set; }
    public string Status { get; set; }
    public DateTime CrearFecha { get; set; }
}