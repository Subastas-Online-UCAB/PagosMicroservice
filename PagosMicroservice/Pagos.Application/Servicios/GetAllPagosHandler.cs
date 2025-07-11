using MediatR;
using Pagos.Application.Queries;
using Pagos.Domain.Entidades;
using Pagos.Domain.Repositorios;

namespace Pagos.Application.Handlers
{
    public class GetAllPagosHandler : IRequestHandler<GetAllPagosQuery, List<Payment>>
    {
        private readonly IMongoPagoRepository _repository;

        public GetAllPagosHandler(IMongoPagoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Payment>> Handle(GetAllPagosQuery request, CancellationToken cancellationToken)
        {
            var payment = await _repository.ObtenerTodasAsync(cancellationToken);
            return payment;
        }
    }
}