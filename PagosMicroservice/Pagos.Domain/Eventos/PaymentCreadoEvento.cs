using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Domain.Eventos
{
    public class PaymentCreadoEvento
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdSubasta { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; } = null!;
        public string PaymentIntentId { get; set; } = null!;
        public decimal PrecioBase { get; set; }
        public DateTime FechaPayment { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
