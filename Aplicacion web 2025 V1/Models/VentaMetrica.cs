using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aplicacion_web_2025_V1.Models
{
    [BsonIgnoreExtraElements]
    public class VentaMetrica
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("facturaId")]
        public int FacturaId { get; set; }

        [BsonElement("clienteId")]
        public string ClienteId { get; set; } = "";

        [BsonElement("clienteNombre")]
        public string ClienteNombre { get; set; } = "";

        [BsonElement("fecha")]
        public DateTime Fecha { get; set; }

        [BsonElement("items")]
        public List<ItemVenta> Items { get; set; } = new();
    }

    public class ItemVenta
    {
        [BsonElement("productoId")]
        public string ProductoId { get; set; } = "";

        [BsonElement("productoNombre")]
        public string ProductoNombre { get; set; } = "";

        [BsonElement("cantidad")]
        [BsonRepresentation(BsonType.Double)]
        public decimal Cantidad { get; set; }
    }
}