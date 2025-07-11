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

            var update = Builders<PagoDocument>.Update
                .Set(s => s.Estado, evento.Estado)
                .Set(s => s.StripeSessionId, evento.StripeSessionId)
                .Set(s => s.StripePaymentIntentId, evento.StripePaymentIntentId)
                .Set(s => s.RazonFallo, evento.RazonFallo);

            await _mongoContext.Pagos.UpdateOneAsync(filter, update);
        }
    }
}