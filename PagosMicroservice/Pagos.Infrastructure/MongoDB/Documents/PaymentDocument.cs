using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Pagos.Infrastructure.MongoDB.Documents
{
    public class PaymentDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("monto")]
        public decimal Monto { get; set; }

        [BsonElement("estado")]
        public string Estado { get; set; }

        [BsonElement("paymentIntentId")]
        public string PaymentIntendId { get; set; }

        [BsonElement("fechaPayment")]
        public DateTime FechaPayment { get; set; }

        [BsonElement("fechaActualizacion")]
        public DateTime FechaActualizacion { get; set; }

        [BsonElement("idUsuario")]
        [BsonRepresentation(BsonType.String)]
        public Guid IdUsuario { get; set; }

        [BsonElement("idSubasta")]
        [BsonRepresentation(BsonType.String)]
        public Guid IdSubasta { get; set; }
    }
}
