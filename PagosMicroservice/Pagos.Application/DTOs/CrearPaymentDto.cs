using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Application.DTOs;

public class CrearPaymentDto
{
	public string IdUsuario { get; set; }
	public string IdSubasta { get; set; }
	public decimal Monto { get; set; };
}