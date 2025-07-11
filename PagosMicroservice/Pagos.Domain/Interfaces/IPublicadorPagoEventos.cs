using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Domain.Eventos;

namespace Pagos.Domain.Interfaces
{
    public interface IPublicadorPagoEventos
    {
        Task PublicarPagoCreado(PagoCreado evento);

        Task PublicarPagoActualizado(PagoActualizado evento);
    }
}
