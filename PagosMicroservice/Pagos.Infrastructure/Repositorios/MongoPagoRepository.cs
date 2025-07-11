using MongoDB.Driver;
using Pagos.Domain.Entidades;
using Pagos.Domain.Repositorios;

using Pagos.Infrastructure.MongoDB;
using Pagos.Infrastructure.MongoDB.Documents;

namespace Pagos.Infrastructure.Repositorios
{
    public class MongoPagoRepository : IMongoPagoRepository
    {
        private readonly IMongoCollection<PagoDocument> _collection;

        public MongoPagoRepository(IPagoMongoContext context)
        {
            _collection = context.Pagos;
        }

        public async Task<List<Payment>> ObtenerTodasAsync(CancellationToken cancellationToken)
        {
            var documentos = await _collection.Find(_ => true).ToListAsync(cancellationToken);

            return documentos.Select(doc => new Payment
            {
                IdPago = doc.Id,
                Monto = doc.Monto,
                FechaCreacion = doc.FechaCreacion,
                Estado = doc.Estado,
                CorreoUsuario = doc.CorreoUsuario
            }).ToList();

        }
    }
}
