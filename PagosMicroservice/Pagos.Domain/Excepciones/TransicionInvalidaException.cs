using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Domain.Excepciones
{
    public class TransicionInvalidaException : Exception
    {
        public TransicionInvalidaException(string estatusActual, string nuevoEstatus)
            : base($"Transición inválida de '{estatusActual}' a '{nuevoEstatus}'.") { }
    }
}
