using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pagos.Domain.Entidades
{

    public class Payment
    {
        public Guid IdPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
        public string CorreoUsuario { get; set; }

        public void Editar(string estado)
        {
            Estado = estado;
        }

    }
}