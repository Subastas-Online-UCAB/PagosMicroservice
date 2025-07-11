using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Domain.Entidades;
using System.Threading;

namespace Pagos.Domain.Repositorios;

public interface IMongoPagoRepository
{
    Task<List<Payment>> ObtenerTodasAsync(CancellationToken cancellationToken);
}

