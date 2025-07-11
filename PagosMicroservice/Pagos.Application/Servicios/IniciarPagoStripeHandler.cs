using MediatR;
using Microsoft.Extensions.Options;
using Pagos.Application.Commands;
using Pagos.Application.Comun;
using Pagos.Application.DTO;
using Pagos.Application.DTOs;
using Pagos.Domain.Entidades;
using Pagos.Domain.Interfaces;
using Pagos.Domain.Repositorios;

namespace Pagos.Application.servicios
{
    public class IniciarPagoStripeHandler : IRequestHandler<IniciarPagoStripeCommand, MessageResponse>
    {
        private readonly IPagoRepository _repository;
        private readonly StripeConfig _stripeConfig;

        public IniciarPagoStripeHandler(
            IPagoRepository repository,
            StripeConfig stripeConfig = null) // Opcional para desarrollo
        {
            _repository = repository;
            _stripeConfig = stripeConfig ?? new StripeConfig
            {
                PublicKey = "pk_test_temporalKey123",
                SecretKey = "sk_test_temporalSecret123",
                WebhookSecret = "whsec_temporalWebhook123"
            };
        }

        public async Task<MessageResponse> Handle(
            IniciarPagoStripeCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Obtener el pago existente
            var pago = await _repository.ObtenerPorIdAsync(request.PagoId, cancellationToken);
            if (pago == null)
                return MessageResponse.CrearError("Pago no encontrado");

            if (pago.Estado != "pending_payment")
                return MessageResponse.CrearError("El pago no está en estado válido para iniciar el proceso");

            // 2. Simular creación de sesión (esto se reemplazará luego)
            var simulatedSessionId = $"simulated_session_{Guid.NewGuid()}";

            // 3. Actualizar entidad (simulación)
            pago.IniciarPagoStripe(simulatedSessionId);
            await _repository.ActualizarAsync(pago, cancellationToken);

            // 4. Preparar respuesta simulada
            var response = new
            {
                SessionId = simulatedSessionId,
                PublicKey = _stripeConfig.PublicKey,
                Url = $"https://checkout.stripe.com/pay/{simulatedSessionId}",
                Message = "Modo simulación - Servicio Stripe no implementado aún"
            };

            return MessageResponse.CrearExito("Sesión de pago simulada", data: response);
        }
    }
}
