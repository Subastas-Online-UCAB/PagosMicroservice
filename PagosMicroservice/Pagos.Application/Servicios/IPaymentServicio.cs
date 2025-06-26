using Pagos.Application.DTOs;

namespace Pagos.Application.Servicios;

public interface IPaymentServicio
{
    Task<ResultadoPaymentDto> ProcessPaymentAsync(CrearPaymentDto PagoDto);
    Task<ResultadoPaymentDto> GetPaymentByIdAsync(Guid id);
    Task<IEnumerable<ResultadoPaymentDto>> GetPaymentsByIdUsuarioAsync(string IdUsuario);
    Task<IEnumerable<ResultadoPaymentDto>> GetPaymentsByIdSubastaAsync(string IdSubasta);
}