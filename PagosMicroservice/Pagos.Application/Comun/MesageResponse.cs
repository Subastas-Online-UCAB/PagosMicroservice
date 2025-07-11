using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Application.Comun
{
    public class MessageResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } // Nuevo campo para datos adicionales

        public static MessageResponse CrearExito(string message, object data = null) =>
            new() { Success = true, Message = message, Data = data };

        public static MessageResponse CrearError(string message) =>
            new() { Success = false, Message = message };
    }
}

