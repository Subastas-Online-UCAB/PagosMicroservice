using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pagos.Application.Comun;

namespace Pagos.Application.Commands
{
    public class ActualizarPagoCommand : IRequest<MessageResponse>
    {
        public Guid PagoId { get; set; }
        public string Estado { get; set; }
    }

    public ActualizarPagoCommand(Guid pagoId, string nuevoEstado)
        {
            PagoId = pagoId;
            Estado = nuevoEstado;
        }
    }
}
