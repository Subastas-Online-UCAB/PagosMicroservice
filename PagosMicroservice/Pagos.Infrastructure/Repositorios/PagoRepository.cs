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
        private readonly IPagoMongoContext _mongoContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public PagoRepository(ApplicationDbContext context, IPagoMongoContext mongoContext, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mongoContext = mongoContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> CrearAsync(Payment pago, CancellationToken cancellationToken)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync(cancellationToken);
            return pago.IdPago;
        }

        public async Task<Payment?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Pagos.FirstOrDefaultAsync(s => s.IdPago == id);
        }

        public async Task ActualizarAsync(Payment pago)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Pagos
                .FirstOrDefaultAsync(s => s.IdPago == id, cancellationToken);
        }

        public async Task ActualizarAsync(Payment pago, CancellationToken cancellationToken)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Payment?> ObtenerPagoCompletoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var doc = await _mongoContext.Pagos
                .Find(s => s.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (doc is null) return null;

            return new Payment
            {
                IdPago = doc.Id,
                Monto = doc.Monto,
                FechaCreacion = doc.FechaCreacion,
                Estado = doc.Estado,
                CorreoUsuario = doc.CorreoUsuario
            };
        }


    }
}
