using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Pagos.Domain.Eventos;

namespace Pagos.Application.Sagas
{
    public class PagoStateMachine : MassTransitStateMachine<PagoState>
    {
        public State Active { get; private set; } = null!;
        public State Canceled { get; private set; } = null!;

    }
}