using MassTransit;
using Pagos.Domain.Eventos;
using Pagos.Infrastructure.Mongo;
using Pagos.Infrastructure.MongoDB;
using Pagos.Infrastructure.MongoDB.Documents;

namespace Pagos.Infrastructure.Consumidor
{
    public class PagoCreadoConsumidor : IConsumer<PagoCreado>
    {
        private readonly IPagoMongoContext _context;

        public PagoCreadoConsumidor(IPagoMongoContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<PagoCreado> context)
        {
            var mensaje = context.Message;

            var documento = new PagoDocument
            {
                Id = mensaje.Id,
                Monto = mensaje.Monto,
                FechaCreacion = mensaje.FechaCreacion,
                Estado = mensaje.Estado,
                CorreoUsuario = mensaje.CorreoUsuario
            };

            await _context.Pagos.InsertOneAsync(documento);
        }
    }
}
