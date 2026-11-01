using Aplicacion_web_2025_V1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class LogService
{
    private readonly IMongoCollection<LogMetrica> _logs;

    public LogService(IMongoClient client, IOptions<MongoDbSettings> options)
    {
        var db = client.GetDatabase(options.Value.DatabaseName);
        _logs = db.GetCollection<LogMetrica>("logs");
    }

    public Task RegistrarAsync(string tipo, string mensaje, string? usuario = null,
        Dictionary<string, string>? detalle = null) =>
        _logs.InsertOneAsync(new LogMetrica
        {
            Tipo = tipo,
            Mensaje = mensaje,
            Usuario = usuario,
            Detalle = detalle
        });

    public Task<List<LogMetrica>> ObtenerAsync(string? tipo, int limite = 50)
    {
        var filtro = string.IsNullOrEmpty(tipo)
            ? Builders<LogMetrica>.Filter.Empty
            : Builders<LogMetrica>.Filter.Eq(l => l.Tipo, tipo);

        return _logs.Find(filtro)
                    .SortByDescending(l => l.Fecha)
                    .Limit(limite)
                    .ToListAsync();
    }
}