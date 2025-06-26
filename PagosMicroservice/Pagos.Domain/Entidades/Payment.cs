using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pagos.Domain.Entidades;

public class Payment
{
    public Guid IdPayment { get; set; }
    public Guid IdUsuario { get; set; } // IdUsuario del microservicio de usuarios
    public Guid IdSubasta { get; set; } // IdSubasta del microservicio de subastas
    public decimal Monto { get; set; }
    // public string Currency { get; set; } = "usd";      //Si queremos especificar tipo de moneda

    public string Estado { get; set; }
    public string PaymentIntentId { get; set; } // ID de Stripe
    //public string PaymentMethod { get; set; }           //otros tipos de pago
    public DateTime  FechaPayment { get; set; } = DateTime.UtcNow;
    public DateTime? FechaActualizacion { get; set; }

    public void Editar(string estado, DateTime fechaActualizacion)
    {
        Estado = estado;
        FechaActualizacion = fechaActualizacion;
    
    }

    public enum EstadoPayment
    {
        EnCurso,
        Completado
    }
}