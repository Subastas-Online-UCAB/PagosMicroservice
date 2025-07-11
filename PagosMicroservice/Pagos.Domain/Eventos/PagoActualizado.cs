using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Domain.Entidades;

namespace Pagos.Domain.Eventos
{
    public class PagoActualizado
    {
        public Guid Id { get; }
        public string Estado { get; }
        public string StripeSessionId { get; }
        public string StripePaymentIntentId { get; }
        public string RazonFallo { get; }

        public PagoActualizado(Payment payment)
        {
            Id = payment.IdPago;
            Estado = payment.Estado;
            StripeSessionId = payment.StripeSessionId;
            StripePaymentIntentId = payment.StripePaymentIntentId;
            RazonFallo = payment.RazonFallo;
        }
    }
}
