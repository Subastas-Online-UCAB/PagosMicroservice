using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Pagos.Domain.Eventos;
using Pagos.Domain.Interfaces;

namespace Pagos.Infrastructure.EventPublishers
{
    public class PublicadorPaymentEventos : IPublicadorPaymentEventos
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublicadorPaymentEventos(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublicarPaymentCreado(PaymentCreadoEvento evento)
        {
            await _publishEndpoint.Publish(evento);
        }
    }
}
