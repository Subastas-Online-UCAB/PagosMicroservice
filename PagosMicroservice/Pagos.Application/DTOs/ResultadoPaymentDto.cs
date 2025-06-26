using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Application.DTOs;

public class ResultadoPaymentDto
{
    public Guid Id { get; set; }
    public string IdUsuario { get; set; }
    public string IdSubasta { get; set; }
    public decimal Monto { get; set; }
    public string PaymentIntentId { get; set; }
    public string Status { get; set; }
    public DateTime CrearFecha { get; set; }
}