using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Application.Commands
{
    public class CreatePaymentCommand : IRequest<Guid>
    {
        public decimal Monto { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string PaymentIntendId { get; set; }
        public DateTime FechaPayment { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdSubasta { get; set; }
    }
}
