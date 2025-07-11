using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Pagos.Domain.Eventos;
using Pagos.Domain.Interfaces;


namespace Pagos.Infrastructure.EventPublisher
{
    public class PublicadorPagoEventos : IPublicadorPagoEventos
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublicadorPagoEventos(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublicarPagoCreado(PagoCreado evento)
        {
            await _publishEndpoint.Publish(evento);
        }

        public async Task PublicarPagoActualizado(PagoActualizado evento)
        {
            await _publishEndpoint.Publish(evento);
        }
    }
}