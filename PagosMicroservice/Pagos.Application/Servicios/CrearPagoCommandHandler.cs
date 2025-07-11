using MediatR;
using Pagos.Application.Commands;
using Pagos.Domain.Entidades;
using Pagos.Domain.Repositorios;
using Pagos.Domain.Interfaces;
using Pagos.Domain.Eventos;

namespace Pagos.Application.servicios
{
    public class CrearPagoCommandHandler : IRequestHandler<CrearPagoCommand, Guid>
    {
        private readonly IPagoRepository _repository;
        private readonly IPublicadorPagoEventos _publisher;

        public CrearPagoCommandHandler(IPagoRepository repository, IPublicadorPagoEventos publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CrearPagoCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                IdPago = Guid.NewGuid(),
                Monto = request.Monto,
                FechaCreacion = request.FechaCreacion,
                Estado = "Pending",
                CorreoUsuario = request.CorreoUsuario
            };

            // 1. Persistencia en base de datos principal (PostgreSQL)
            await _repository.CrearAsync(payment, cancellationToken);

            // 2. Publicar evento general (por ejemplo, para vistas o proyecciones)
            var eventoCreado = new PagoCreado
            {
                Id = payment.IdPago,
                Monto = payment.Monto,
                FechaCreacion = payment.FechaCreacion,
                Estado = "Pending",
                CorreoUsuario = payment.CorreoUsuario
            };

            await _publisher.PublicarPagoCreado(eventoCreado);

            return payment.IdPago;
        }
    }
}
