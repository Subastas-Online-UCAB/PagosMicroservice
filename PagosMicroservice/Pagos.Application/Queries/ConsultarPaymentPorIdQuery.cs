using MediatR;
using Pagos.Application.DTOs;
using Pagos.Domain.Entidades;

namespace Pagos.Application.Queries
{
    public class ConsultarPaymentPorIdQuery : IRequest<ResultadoPaymentDto?>
    {
        public Guid IdPayment { get; }

        public ConsultarPaymentPorIdQuery(Guid idPayment)
        {
            IdPayment = idPayment;
        }
    }
}
