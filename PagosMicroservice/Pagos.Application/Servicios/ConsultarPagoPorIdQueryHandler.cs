using MediatR;
using Pagos.Application.DTO;
using Pagos.Application.Queries;
using Pagos.Domain.Entidades;
using Pagos.Domain.Repositorios;

namespace Pagos.Application.Handlers
{
    public class ConsultarPagoPorIdQueryHandler : IRequestHandler<ConsultarPagoPorIdQuery, PagoDto?>
    {
        private readonly IPagoRepository _repository;

        public ConsultarPagoPorIdQueryHandler(IPagoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagoDto?> Handle(ConsultarPagoPorIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _repository.ObtenerPorIdAsync(request.IdPago, cancellationToken);

            if (payment == null)
                return null;

            return new PagoDto
            {
                Id = payment.IdPago,
                Monto = payment.Monto,
                FechaCreacion = payment.FechaCreacion,
                Estado = payment.Estado,
                CorreoUsuario = payment.CorreoUsuario,
                StripePaymentIntentId = payment.StripePaymentIntentId,
                StripeSessionId = payment.StripeSessionId
            };
        }

    }
}