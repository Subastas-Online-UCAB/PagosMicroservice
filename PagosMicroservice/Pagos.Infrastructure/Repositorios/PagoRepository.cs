using Pagos.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Infrastructure.Persistencia;
using Pagos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Pagos.Domain.Eventos;
using Pagos.Infrastructure.MongoDB.Documents;
using MassTransit;
using Pagos.Infrastructure.Mongo;
using Pagos.Application.DTO;
using Pagos.Infrastructure.MongoDB;
using Pago.Infrastructure.Persistencia;

namespace Pagos.Infrastructure.Repositorios
{
    public class PagoRepository : IPagoRepository
    {
        private readonly ApplicationDbContext _context;

        public PagoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CrearAsync(Payment pago, CancellationToken cancellationToken)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync(cancellationToken);
            return pago.IdPago;
        }

        public async Task<Payment?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Pagos
                .FirstOrDefaultAsync(p => p.IdPago == id, cancellationToken);
        }

        public async Task ActualizarAsync(Payment pago, CancellationToken cancellationToken)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Payment?> ObtenerPagoCompletoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Pagos
                .FirstOrDefaultAsync(p => p.IdPago == id, cancellationToken);
        }
    }
}
