using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagos.Domain.Entidades;

namespace UsuarioServicio.Infrastructure.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Payment> Payment { get; set; }

        // Opcionalmente, para ver las tablas creadas, puedes sobreescribir OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Opcional: Cambiar nombre de la tabla si quieres
            modelBuilder.Entity<Payment>().ToTable("Payment");

            // Opcional: Configuraciones de columnas si quieres afinar
            modelBuilder.Entity<Payment>(entity =>
            {
               // entity.ToTable("Subastas");

                entity.HasKey(s => s.IdPayment);

                entity.Property(s => s.Monto)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(s => s.Estado)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(s => s.Estado)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(s => s.FechaPayment)
                    .IsRequired();


                entity.Property(s => s.IdUsuario)
                    .IsRequired();

                entity.Property(s => s.IdSubasta)
                    .IsRequired();
            });
        }
    }
}

