using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class LogMetrica
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string? Id { get; set; }

    [BsonElement("tipo")]
    public string Tipo { get; set; } = "";

    [BsonElement("mensaje")]
    public string Mensaje { get; set; } = "";

    [BsonElement("usuario")]
    public string? Usuario { get; set; }

    [BsonElement("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    [BsonElement("detalle")]
    public Dictionary<string, string>? Detalle { get; set; }
}