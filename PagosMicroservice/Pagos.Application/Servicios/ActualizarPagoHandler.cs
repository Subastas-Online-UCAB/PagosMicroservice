using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Application.Commands;
using Pagos.Domain.Eventos;
using Pagos.Application.Comun;
using Pagos.Domain.Entidades;
using Pagos.Domain.Repositorios;
using Pagos.Domain.Interfaces;

namespace Pagos.Aplicacion.Servicios
{ 

    public class ActualizarPagoHandler : IRequestHandler<ActualizarPagoCommand, MessageResponse>
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IPublicadorPagoEventos _eventPublisher;

        public ActualizarPagoHandler(IPagoRepository pagoRepository, IPublicadorPagoEventos eventPublisher)
        {
            _pagoRepository = pagoRepository;
            _eventPublisher = eventPublisher;

        }

        public async Task<MessageResponse> Handle(ActualizarPagoCommand request, CancellationToken cancellationToken)
        {
            var payment = await _pagoRepository.ObtenerPorIdAsync(request.PagoId, cancellationToken);
            if (payment == null)
                return MessageResponse.CrearError("El Producto no existe.");

            // 3. Actualizar el pago
            payment.Editar(
                request.Estado
            );

            // 4. Persistir cambios
            await _pagoRepository.ActualizarAsync(payment, cancellationToken);

            // 5. Publicar evento de actualización
            await _eventPublisher.PublicarPagoActualizado(new PagoActualizado
            {
                Id = payment.IdPago,
                Monto = payment.Monto,
                FechaCreacion = payment.FechaCreacion,
                Estado = payment.Estado,
                CorreoUsuario = payment.CorreoUsuario,
            });


            return MessageResponse.CrearExito("Pago actualizado exitosamente.");
        }
    }
}
