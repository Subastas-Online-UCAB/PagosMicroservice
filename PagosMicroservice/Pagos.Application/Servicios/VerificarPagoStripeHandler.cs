using MediatR;
using Pagos.Application.Commands;
using Pagos.Application.Comun;
using Pagos.Domain.Entidades;
using Pagos.Domain.Interfaces;
using Pagos.Domain.Repositorios;

namespace Pagos.Application.servicios
{
    public class VerificarPagoStripeHandler : IRequestHandler<VerificarPagoStripeCommand, MessageResponse>
    {
        private readonly IPagoRepository _repository;
        private readonly IStripePaymentService _stripeService;

        public VerificarPagoStripeHandler(
            IPagoRepository repository,
            IStripePaymentService stripeService)
        {
            _repository = repository;
            _stripeService = stripeService;
        }

        public async Task<MessageResponse> Handle(
    VerificarPagoStripeCommand request,
    CancellationToken cancellationToken)
        {
            // 1. Obtener pago
            var pago = await _repository.ObtenerPorIdAsync(request.PagoId, cancellationToken);
            if (pago == null)
                return MessageResponse.CrearError("Pago no encontrado");

            // 2. Validar que tenga sesión de Stripe
            if (string.IsNullOrEmpty(pago.StripeSessionId))
                return MessageResponse.CrearError("Este pago no tiene sesión de Stripe");

            try
            {
                // 3. Verificar pago con Stripe
                var resultado = await _stripeService.VerifyPaymentAsync(pago.StripeSessionId);

                if (resultado.EsValido)
                {
                    pago.ConfirmarPago();
                    await _repository.ActualizarAsync(pago, cancellationToken);
                    return MessageResponse.CrearExito("Pago confirmado exitosamente");
                }

                // 4. Manejar fallo (versión corregida)
                pago.MarcarComoFallido(resultado.Error); // Ahora acepta el parámetro
                await _repository.ActualizarAsync(pago, cancellationToken);
                return MessageResponse.CrearError($"Pago no completado: {resultado.Error}");
            }
            catch (Exception ex)
            {
                pago.MarcarComoFallido($"Error al verificar: {ex.Message}");
                await _repository.ActualizarAsync(pago, cancellationToken);
                return MessageResponse.CrearError($"Error al verificar pago: {ex.Message}");
            }
        }
    }
}