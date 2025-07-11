using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Domain.Entidades;
using System.Threading.Tasks;

namespace Pagos.Domain.Repositorios
{
    public interface IPagoRepository
    {
        Task<Guid> CrearAsync(Payment pago, CancellationToken cancellationToken);

        Task<Payment?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);

        Task ActualizarAsync(Payment pago, CancellationToken cancellationToken);

        Task<Payment?> ObtenerPagoCompletoPorIdAsync(Guid id, CancellationToken cancellationToken);

    }
}