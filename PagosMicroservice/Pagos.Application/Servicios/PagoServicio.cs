using Pagos.Domain.Entidades;
using Pagos.Domain.Interfaces;
using Pagos.Application.DTOs;
using Pagos.Infrastructure.Servicios;

namespace Pagos.Application.Servicios;

public class PagotServicio : IPagoServicio
{
    private readonly IPagoRepositorio _pagoRepositorio;
    private readonly StripeServicio _stripeServicio;

    public PagoServicio(IPagoRepositorio pagoRepositorio, StripeServicio stripeServicio)
    {
        _pagoRepositorio = pagoRepositorio;
        _stripeServicio = stripeServicio;
    }

    public async Task<ResultadoPagoDto> ProcessPaymentAsync(CrearPagoDto pagoDto)
    {
        // Crear el pago en Stripe
        var paymentIntent = await _stripeServicio.CreatePaymentIntentAsync(
            pagoDto.Cantidad,
            pagoDto.IdUsuario,
            pagoDto.IdSubasta);

        // Guardar en nuestra base de datos
        var pago = new Pago
        {
            IdUsuario = pagoDto.IdUsuario,
            IdSubasta = pagoDto.IdSubasta,
            Cantidad = pagoDto.Cantidad,
            PaymentIntentId = paymentIntent.Id,
            //PaymentMethod = "card", // Asumimos tarjeta por defecto
            Status = paymentIntent.Status
        };

        await _pagoRepositorio.AddAsync(pago);

        return MapToDto(pago);
    }

    public async Task<ResultadoPagoDto> GetPaymentByIdAsync(Guid id)
    {
        var pago = await _pagoRepositorio.GetByIdAsync(id);
        if (pago == null) return null;

        // Actualizar estado desde Stripe
        var paymentIntent = await _stripeServicio.GetPaymentIntentAsync(pago.PaymentIntentId);
        pago.Status = paymentIntent.Status;
        await _pagoRepositorio.UpdateAsync(pago);

        return MapToDto(pago);
    }

    public async Task<IEnumerable<PaymentResultDto>> GetPaymentsByIdUsuarioAsync(string IdUsuario)
    {
        var pago = await _pagoRepositorio.GetByIdUsuarioAsync(IdUsuario);
        return pago.Select(MapToDto);
    }

    public async Task<IEnumerable<ResultadoPagoDto>> GetPaymentsByIdSubastaAsync(string idsubasta)
    {
        var pago = await _pagoRepositorio.GetByIdSubastaAsync(idsubasta);
        return pago.Select(MapToDto);
    }

    private ResultadoPagoDto MapToDto(Payment payment)
    {
        return new ResultadoPagoDto
        {
            Id = payment.Id,
            IdUsuario = payment.IdUsuario,
            IdSubasta = payment.IdSubasta,
            cantidad = payment.Cantidad,
            PaymentIntentId = payment.PaymentIntentId,
            Status = payment.Status,
            CrearFecha = payment.CrearFecha
        };
    }
}