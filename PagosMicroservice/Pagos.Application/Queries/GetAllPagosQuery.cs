using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Pagos.Application.DTO;
using Pagos.Domain.Entidades;

namespace Pagos.Application.Queries
{
    
    public class GetAllPagosQuery : IRequest<List<Payment>> { }

}
