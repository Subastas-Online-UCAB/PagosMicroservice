using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Domain.Entidades;

namespace Pagos.Domain.Eventos
{
    public class PagoFallido
    {
        public Guid Id { get; }
        public string Razon { get; }

        public PagoFallido(Guid id, string razon)
        {
            Id = id;
            Razon = razon;
        }
    }
}
