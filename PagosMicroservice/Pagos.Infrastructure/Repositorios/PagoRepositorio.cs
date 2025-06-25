using Microsoft.EntityFrameworkCore;
using Pagos.Domain.Entidades;
using Pagos.Domain.Interfaces;

namespace Pagos.Infrastructure.Repositorios;

public class PagoRepositorio : IPagoRopositorio
{
    private readonly PagoDbContext _context;

    public PagoRepositorio(PagoDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> GetByIdAsync(Guid id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task<IEnumerable<Payment>> GetByIdUsuarioAsync(string IdUsuario)
    {
        return await _context.Payments.Where(p => p.IdUsuario == IdUsuario).ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByIdSubastaAsync(string IdSubasta)
    {
        return await _context.Payments.Where(p => p.IdSubasta == IdSubasta).ToListAsync();
    }

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Payment payment)
    {
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
    }
}