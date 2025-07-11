using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Pagos.Infrastructure.MongoDB.Documents
{
    public class PagoDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("monto")]
        public decimal Monto { get; set; }

        [BsonElement("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [BsonElement("estado")]
        public string Estado { get; set; }

        [BsonElement("correoUsuario")]
        public string CorreoUsuario { get; set; }

        // Nuevos campos para Stripe
        [BsonElement("stripeSessionId")]
        public string StripeSessionId { get; set; }

        [BsonElement("stripePaymentIntentId")]
        public string StripePaymentIntentId { get; set; }

        [BsonElement("razonFallo")]
        public string RazonFallo { get; set; }
    }
}