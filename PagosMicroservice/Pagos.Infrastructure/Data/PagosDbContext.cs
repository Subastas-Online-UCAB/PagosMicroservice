using Microsoft.EntityFrameworkCore;
using Pagos.Domain.Entidades;

namespace Pagos.Infrastructure.Data;

public class PagoDbContext : DbContext
{
    public PagoDbContext(DbContextOptions<PagoDbContext> options) : base(options)
    {
    }

    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        });
    }
}