namespace Pagos.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public string IdUsuario { get; set; } // IdUsuario del microservicio de usuarios
    public string IdSubasta { get; set; } // IdSubasta del microservicio de subastas
    public decimal cantidad { get; set; }
    // public string Currency { get; set; } = "usd";      //Si queremos especificar tipo de moneda
    public string PaymentIntentId { get; set; } // ID de Stripe
    //public string PaymentMethod { get; set; }           //otros tipos de pago
    public string Status { get; set; }
    public DateTime  CrearFecha { get; set; } = DateTime.UtcNow;
    public DateTime? ActualizarFecha { get; set; }
}