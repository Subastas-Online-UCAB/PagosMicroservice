using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Pagos.Application.Commands
{
    public class CrearPagoCommand : IRequest<Guid>
    {
        public decimal Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string CorreoUsuario { get; set; }
    }
}
