using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Pagos.Application.Comun;

namespace Pagos.Application.Commands
{
    public class IniciarPagoStripeCommand : IRequest<MessageResponse>
    {
        public Guid PagoId { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
