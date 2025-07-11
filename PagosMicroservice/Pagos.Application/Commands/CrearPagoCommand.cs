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
        public string Moneda { get; set; } = "usd";
        public string Descripcion { get; set; } 

        public string CorreoUsuario { get; set; }
    }
}
