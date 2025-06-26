using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Domain.Entidades;

namespace Pagos.Domain.Repositorios;

public interface IPaymentRepositorio
{
    Task<Guid> CrearAsync(Payment payment, CancellationToken cancellationToken);
    Task<Payment?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetByIdUsuarioAsync(string IdUsuario, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetByIdsubastaAsync(string IdSubasta, CancellationToken cancellationToken);
    Task AddAsync(Payment payment, CancellationToken cancellationToken);
    Task UpdateAsync(Payment payment, CancellationToken cancellationToken);
}