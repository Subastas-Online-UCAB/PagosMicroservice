using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pagos.Domain.Entidades
{
    public class Payment
    {
        public Guid IdPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
        public string CorreoUsuario { get;set; }
        public string? StripePaymentIntentId { get; set; } // Nuevo campo para Stripe
        public string? StripeSessionId { get; set; } // Nuevo campo para checkout de Stripe

        public string RazonFallo { get; set; }

        // Constructor privado para encapsulación
        public Payment() { }

        // Factory Method para creación
        public static Payment Create(decimal monto, string correoUsuario)
        {
            return new Payment
            {
                IdPago = Guid.NewGuid(),
                Monto = monto,
                FechaCreacion = DateTime.UtcNow,
                Estado = "pending_payment", // Nuevo estado inicial
                CorreoUsuario = correoUsuario
            };
        }

        // Métodos para Stripe
        public void IniciarPagoStripe(string sessionId)
        {
            this.StripeSessionId = sessionId;
            this.Estado = "awaiting_payment"; // O el estado que uses para pagos en proceso
        }

        public void ConfirmarPago()
        {
            Estado = "paid";
            RazonFallo = null; // Limpiamos el fallo previo si existe
        }

        public void MarcarComoFallido(string razon = null)
        {
            Estado = "failed";
            RazonFallo = razon;
        }

        public void MarcarComoFallido() => MarcarComoFallido(null);

        public void Editar(string estado)
        {
            // Validación básica de estados
            var estadosValidos = new[] { "pending_payment", "awaiting_payment", "paid", "failed" };
            if (!estadosValidos.Contains(estado))
                throw new ArgumentException("Estado no válido");

            Estado = estado;
        }
    }
}