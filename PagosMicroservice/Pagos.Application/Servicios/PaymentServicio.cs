using Pagos.Domain.Entidades;
using Pagos.Domain.Interfaces;
using Pagos.Application.DTOs;
using Pagos.Infrastructure.Servicios;

namespace Pagos.Application.Servicios;

public class PaymentServicio : IPaymentServicio
{
    private readonly IPaymentRepositorio _paymentRepositorio;
    private readonly StripeServicio _stripeServicio;

    public PaymentServicio(IPaymentRepositorio paymentRepositorio, StripeServicio stripeServicio)
    {
        _paymentRepositorio = paymentRepositorio;
        _stripeServicio = stripeServicio;
    }

    public async Task<ResultadoPaymentDto> ProcessPaymentAsync(CrearPaymentDto pagoDto)
    {
        // Crear el pago en Stripe
        var paymentIntent = await _stripeServicio.CreatePaymentIntentAsync(
            pagoDto.Monto,
            pagoDto.IdUsuario,
            pagoDto.IdSubasta);

        // Guardar en nuestra base de datos
        var pago = new Payment
        {
            IdUsuario = paymentDto.IdUsuario,
            IdSubasta = paymentDto.IdSubasta,
            Monto = pagoDto.Monto,
            PaymentIntentId = paymentIntent.Id,
            //PaymentMethod = "card", // Asumimos tarjeta por defecto
            Status = paymentIntent.Status
        };

        await _paymentRepositorio.AddAsync(pago);

        return MapToDto(pago);
    }

    public async Task<ResultadoPaymentDto> GetPaymentByIdAsync(Guid id)
    {
        var pago = await _paymentRepositorio.GetByIdAsync(id);
        if (pago == null) return null;

        // Actualizar estado desde Stripe
        var paymentIntent = await _stripeServicio.GetPaymentIntentAsync(pago.PaymentIntentId);
        pago.Status = paymentIntent.Status;
        await _paymentRepositorio.UpdateAsync(pago);

        return MapToDto(pago);
    }

    public async Task<IEnumerable<ResultadoPaymentDto>> GetPaymentsByIdUsuarioAsync(string IdUsuario)
    {
        var pago = await _paymentRepositorio.GetByIdUsuarioAsync(IdUsuario);
        return pago.Select(MapToDto);
    }

    public async Task<IEnumerable<ResultadoPaymentDto>> GetPaymentsByIdSubastaAsync(string idsubasta)
    {
        var pago = await _paymentRepositorio.GetByIdSubastaAsync(idsubasta);
        return pago.Select(MapToDto);
    }

    private ResultadoPaymentDto MapToDto(Payment payment)
    {
        return new ResultadoPaymentDto
        {
            Id = payment.Id,
            IdUsuario = payment.IdUsuario,
            IdSubasta = payment.IdSubasta,
            Monto = payment.Monto,
            PaymentIntentId = payment.PaymentIntentId,
            Status = payment.status,
            CrearFecha = payment.CrearFecha
        };
    }
}