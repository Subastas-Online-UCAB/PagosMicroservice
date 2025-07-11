using MassTransit;
using MongoDB.Driver;
using Pagos.Domain.Eventos;
using Pagos.Infrastructure.MongoDB;
using Pagos.Infrastructure.MongoDB.Documents;

namespace Pagos.Infrastructure.Consumidor
{
    public class PagoActualizadoConsumidor : IConsumer<PagoActualizado>
    {
        private readonly IPagoMongoContext _mongoContext;

        public PagoActualizadoConsumidor(IPagoMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task Consume(ConsumeContext<PagoActualizado> context)
        {
            var evento = context.Message;

            var filter = Builders<PagoDocument>.Filter.Eq(s => s.Id, evento.Id);

            var documentoActual = await _mongoContext.Pagos
                .Find(filter)
                .FirstOrDefaultAsync();


            var updatedDocument = new PagoDocument
            {
                Id = evento.Id,
                Monto = evento.Monto,
                FechaCreacion = evento.FechaCreacion,
                Estado = evento.Estado,
                CorreoUsuario = evento.CorreoUsuario,
            };

            await _mongoContext.Pagos.ReplaceOneAsync(
                filter,
                updatedDocument,
                new ReplaceOptions { IsUpsert = true }
            );
        }
    }
}