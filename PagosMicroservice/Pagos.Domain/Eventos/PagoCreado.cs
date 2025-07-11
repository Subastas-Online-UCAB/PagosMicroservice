using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Domain.Entidades;


namespace Pagos.Domain.Eventos
{
    public class PagoCreado
    {
        public Guid Id { get; }
        public decimal Monto { get; }
        public DateTime FechaCreacion { get; }
        public string Estado { get; }
        public string CorreoUsuario { get; }
        public string StripeSessionId { get; }
        public string StripePaymentIntentId { get; }
        public string RazonFallo { get; }

        public PagoCreado(Payment payment)
        {
            Id = payment.IdPago;
            Monto = payment.Monto;
            FechaCreacion = payment.FechaCreacion;
            Estado = payment.Estado;
            CorreoUsuario = payment.CorreoUsuario;
            StripeSessionId = payment.StripeSessionId;
            StripePaymentIntentId = payment.StripePaymentIntentId;
            RazonFallo = payment.RazonFallo;
        }
    }
}

