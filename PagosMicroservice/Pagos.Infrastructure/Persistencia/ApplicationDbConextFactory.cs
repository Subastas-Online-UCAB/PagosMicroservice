using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pago.Infrastructure.Persistencia;

namespace Pagos.Infrastructure.Persistencia
{
    public class PagoDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PagosDb;Username=postgres;Password=admin");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
    

