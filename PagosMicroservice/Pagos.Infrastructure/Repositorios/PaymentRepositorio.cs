using Pagos.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioServicio.Infrastructure.Persistencia;
using Pagos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Pagos.Domain.Eventos;
using Pagos.Infrastructure.MongoDB.Documents;
using MassTransit;
using Pagos.Infrastructure.Mongo;
using Pagos.Application.DTO;
using Pagos.Infrastructure.MongoDB;

namespace Pagos.Infrastructure.Repositorios;

public class PaymentRepositorio : IPagoRopositorio
{
    private readonly PagoDbContext _context;

    public PaymentRepositorio(PagoDbContext context)
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