using Pagos.Application.DTOs;

namespace Pagos.Application.Servicios;

public interface IPagoServicio
{
    Task<ResultadoPagoDto> ProcessPaymentAsync(CreatePaymentDto PagoDto);
    Task<ResultadoPagoDto> GetPaymentByIdAsync(Guid id);
    Task<IEnumerable<ResultadoPagoDto>> GetPaymentsByIdUsuarioAsync(string IdUsuario);
    Task<IEnumerable<ResultadoPagoDto>> GetPaymentsByIdSubastaAsync(string IdSubasta);
}