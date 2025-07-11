using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagos.Domain.Entidades;

namespace Pago.Infrastructure.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Payment> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Opcional: Cambiar nombre de la tabla si quieres
            modelBuilder.Entity<Payment>().ToTable("Pagos");

            // Opcional: Configuraciones de columnas si quieres afinar
            modelBuilder.Entity<Payment>(entity =>
            {
                // entity.ToTable("Pagos");

                entity.HasKey(s => s.IdPago);

                entity.Property(s => s.Monto)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(s => s.FechaCreacion)
                    .IsRequired();

                entity.Property(s => s.Estado)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(s => s.CorreoUsuario)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}

