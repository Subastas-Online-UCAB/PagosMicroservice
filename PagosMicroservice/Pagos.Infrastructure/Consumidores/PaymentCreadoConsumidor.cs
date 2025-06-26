using MassTransit;
using Pagos.Domain.Eventos;
using Pagos.Infrastructure.Mongo;
using Pagos.Infrastructure.MongoDB;
using Pagos.Infrastructure.MongoDB.Documents;

namespace Pagos.Infrastructure.Consumidores
{
    public class PaymentCreadoConsumidor : IConsumer<PaymentCreadoEvento>
{
    private readonly IPaymentMongoContext _context;

    public PaymentCreadoConsumidor(IPaymentMongoContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<PaymentCreadoEvento> context)
    {
        var mensaje = context.Message;

        var documento = new PaymentDocument
        {
            Id = mensaje.Id,
            Monto = mensaje.Monto,
            Estado = mensaje.Estado,
            FechaPayment = mensaje.FechaPayment,
            FechaActualizacion = mensaje.FechaActualizacion,
            IdUsuario = mensaje.IdUsuario,
            IdSubasta = mensaje.IdSubasta
        };

        await _context.Payment.InsertOneAsync(documento);
    }
}
}
