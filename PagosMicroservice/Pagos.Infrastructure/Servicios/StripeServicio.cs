using Stripe;
using Pagos.Domain.Entidades;

namespace Pagos.Infrastructure.Servicios;

public class StripeServicio
{
    private readonly string _stripeSecretKey;

    public StripeServicio(IConfiguration configuration)
    {
        _stripeSecretKey = configuration["Stripe:SecretKey"];
        StripeConfiguration.ApiKey = _stripeSecretKey;
    }

    public async Task<PaymentIntent> CreatePaymentIntentAsync(decimal cantidad, string IdUsuario, string IdSubasta)
    {
        var options = new PaymentIntentCreateOptions
        {
            Monto = (long)(monto * 100), // Stripe usa centavos
            Metadata = new Dictionary<string, string>
            {
                { "IdUsuario", IdUsuario },
                { "IdSubasta", IdSubasta }
            }
        };

        var service = new PaymentIntentService();
        return await service.CreateAsync(options);
    }

    public async Task<PaymentIntent> GetPaymentIntentAsync(string paymentIntentId)
    {
        var service = new PaymentIntentService();
        return await service.GetAsync(paymentIntentId);
    }
}