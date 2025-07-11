using Stripe;
using Microsoft.Extensions.Options;
using Pagos.Domain.Interfaces;
using Pagos.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stripe.Checkout;

namespace Pagos.Infrastructure.Stripe
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly StripeConfig _config;

        public StripePaymentService(IOptions<StripeConfig> config)
        {
            _config = config.Value;
            StripeConfiguration.ApiKey = _config.SecretKey;
        }

        public string GetWebhookSecret()
        {
            return _config.WebhookSecret;
        }

        public async Task<string> CreatePaymentSessionAsync(
            decimal amount,
            string currency,
            string customerEmail,
            string successUrl,
            string cancelUrl,
            Dictionary<string, string> metadata = null)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100), // Stripe usa centavos
                            Currency = currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Pago de servicio",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                CustomerEmail = customerEmail,
                Metadata = metadata ?? new Dictionary<string, string>()
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Id;
        }

        public async Task<(bool EsValido, string Error)> VerifyPaymentAsync(string sessionId)
        {
            try
            {
                var service = new SessionService();
                var session = await service.GetAsync(sessionId);

                if (session.PaymentStatus == "paid")
                    return (true, null);

                return (false, session.PaymentStatus);
            }
            catch (StripeException ex)
            {
                return (false, ex.Message);
            }
        }
    }
}