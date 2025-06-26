using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Domain.Eventos
{
    public class PaymentFinalizadoEvento
    {
        public Guid Id { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
