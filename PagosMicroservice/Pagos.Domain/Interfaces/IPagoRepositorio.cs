namespace Pagos.Domain.Interfaces;

public interface IPagoRepositorio
{
    Task<Payment> GetByIdAsync(Guid id);
    Task<IEnumerable<Payment>> GetByIdUsuarioAsync(string IdUsuario);
    Task<IEnumerable<Payment>> GetByIdsubastaAsync(string IdSubasta);
    Task AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
}