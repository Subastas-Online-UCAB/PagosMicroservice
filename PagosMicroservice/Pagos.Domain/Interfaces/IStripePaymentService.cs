using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagos.Domain.Eventos;

namespace Pagos.Domain.Interfaces
{
    public interface IStripePaymentService
    {
        /// <summary>
        /// Crea una sesión de pago en Stripe
        /// </summary>
        Task<string> CreatePaymentSessionAsync(
            decimal amount,
            string currency,
            string customerEmail,
            string successUrl,
            string cancelUrl,
            Dictionary<string, string> metadata = null);

        /// <summary>
        /// Verifica el estado de un pago en Stripe
        /// </summary>
        Task<(bool EsValido, string Error)> VerifyPaymentAsync(string sessionId);

        /// <summary>
        /// Obtiene el secreto para validar webhooks de Stripe
        /// </summary>
        string GetWebhookSecret();
    }
}