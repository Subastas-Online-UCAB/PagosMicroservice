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

        public CrearPagoCommandHandler(
            IPagoRepository repository,
            IPublicadorPagoEventos publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(
            CrearPagoCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Crear la entidad Payment usando el factory method
            var payment = Payment.Create(
                request.Monto,
                request.CorreoUsuario);

            // 2. Persistir en la base de datos principal
            await _repository.CrearAsync(payment, cancellationToken);

            // 3. Publicar evento de dominio
            await _publisher.PublicarPagoCreado(new PagoCreado(payment));

            // 4. Retornar ID del pago creado
            return payment.IdPago;
        }
    }
}