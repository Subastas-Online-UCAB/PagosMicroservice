using MediatR;
using Pagos.Application.DTO;
using Pagos.Domain.Entidades;

namespace Pagos.Application.Queries
{
    public class ConsultarPagoPorIdQuery : IRequest<PagoDto?>
    {
        public Guid IdPago { get; }

        public ConsultarPagoPorIdQuery(Guid idPago)
        {
            IdPago = idPago;
        }
    }
}