using Aplicacion_web_2025_V1.Context;
using Aplicacion_web_2025_V1.DTOs;
using Aplicacion_web_2025_V1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MetricasService
{
    private readonly IMongoCollection<VentaMetrica> _ventas;
    private readonly LogService _log;

    public MetricasService(IMongoClient client, IOptions<MongoDbSettings> options, LogService log)
    {
        var db = client.GetDatabase(options.Value.DatabaseName);
        _ventas = db.GetCollection<VentaMetrica>(options.Value.VentasCollection);
        _log = log;
    }

    // SINCRONIZACIÓN: copia facturas reales (SQL) a Mongo, con nombres
    public async Task<int> SincronizarAsync(AppDbContext db)
    {
        var facturas = await db.Factura
            .AsNoTracking()
            .Select(f => new { f.id_factura, f.id_cliente, f.fecha_venta })
            .ToListAsync();

        var detalles = (await db.Database
            .SqlQueryRaw<DetalleLectura>(
                @"SELECT id_factura AS IdFactura,
                         id_producto AS IdProducto,
                         CAST(cantidad AS decimal(18,2)) AS Cantidad
                  FROM Detalle_Factura")
            .ToListAsync())
            .GroupBy(d => d.IdFactura)
            .ToDictionary(g => g.Key, g => g.ToList());

        var clientes = (await db.Database
            .SqlQueryRaw<NombreLectura>(
                "SELECT id_cliente AS Id, nombre AS Nombre FROM Cliente")
            .ToListAsync())
            .ToDictionary(c => c.Id, c => c.Nombre.Trim());

        var productos = (await db.Database
            .SqlQueryRaw<NombreLectura>(
                "SELECT id_producto AS Id, nombre AS Nombre FROM Producto")
            .ToListAsync())
            .ToDictionary(p => p.Id, p => p.Nombre.Trim());

        foreach (var f in facturas)
        {
            var doc = new VentaMetrica
            {
                FacturaId = f.id_factura,
                ClienteId = f.id_cliente.ToString(),
                ClienteNombre = clientes.GetValueOrDefault(f.id_cliente, "Sin nombre"),
                Fecha = DateTime.SpecifyKind(f.fecha_venta, DateTimeKind.Utc),
                Items = detalles.TryGetValue(f.id_factura, out var lista)
                    ? lista.Select(d => new ItemVenta
                    {
                        ProductoId = d.IdProducto.ToString(),
                        ProductoNombre = productos.GetValueOrDefault(d.IdProducto, "Sin nombre"),
                        Cantidad = d.Cantidad
                    }).ToList()
                    : new List<ItemVenta>()
            };

            await _ventas.ReplaceOneAsync(
                x => x.FacturaId == f.id_factura,
                doc,
                new ReplaceOptions { IsUpsert = true });
        }

        await _log.RegistrarAsync("sincronizacion", "Facturas copiadas a Mongo",
            detalle: new Dictionary<string, string>
            {
                { "facturas", facturas.Count.ToString() }
            });

        return facturas.Count;
    }

    // MÉTRICA 1: cantidad promedio de un producto por cliente en N semanas
    public async Task<List<PromedioProductoClienteDto>> PromedioProductoClienteAsync(
        DateTime inicio, int semanas)
    {
        var fin = inicio.AddDays(7 * semanas);

        var ventas = await _ventas
            .Find(v => v.Fecha >= inicio && v.Fecha < fin)
            .ToListAsync();

        var resultado = ventas
            .SelectMany(v => v.Items.Select(i => new
            {
                v.ClienteId,
                v.ClienteNombre,
                i.ProductoId,
                i.ProductoNombre,
                i.Cantidad,
                Semana = Math.Min((int)((v.Fecha - inicio).TotalDays / 7), semanas - 1)
            }))
            .GroupBy(x => new { x.ClienteId, x.ClienteNombre, x.ProductoId, x.ProductoNombre })
            .Select(g =>
            {
                var porSemana = new decimal[semanas];
                foreach (var x in g) porSemana[x.Semana] += x.Cantidad;

                return new PromedioProductoClienteDto
                {
                    Cliente = g.Key.ClienteId,
                    ClienteNombre = g.Key.ClienteNombre,
                    Producto = g.Key.ProductoId,
                    ProductoNombre = g.Key.ProductoNombre,
                    Semanas = porSemana,
                    Promedio = Math.Round(porSemana.Average(), 2)
                };
            })
            .ToList();

        await _log.RegistrarAsync("metrica_promedio_producto_cliente",
            "Se consultó el promedio de producto por cliente",
            detalle: new Dictionary<string, string>
            {
                { "inicio", inicio.ToString("yyyy-MM-dd") },
                { "semanas", semanas.ToString() },
                { "registros", resultado.Count.ToString() }
            });

        return resultado;
    }

    // MÉTRICA 2: productos más vendidos
    public async Task<List<ProductoMasVendidoDto>> ProductosMasVendidosAsync(int limite)
    {
        var ventas = await _ventas.Find(_ => true).ToListAsync();

        var resultado = ventas
            .SelectMany(v => v.Items)
            .GroupBy(i => new { i.ProductoId, i.ProductoNombre })
            .Select(g => new ProductoMasVendidoDto
            {
                Producto = g.Key.ProductoId,
                ProductoNombre = g.Key.ProductoNombre,
                CantidadTotal = g.Sum(i => i.Cantidad)
            })
            .OrderByDescending(p => p.CantidadTotal)
            .Take(limite)
            .ToList();

        await _log.RegistrarAsync("metrica_productos_mas_vendidos",
            "Se consultaron los productos más vendidos",
            detalle: new Dictionary<string, string>
            {
                { "limite", limite.ToString() },
                { "registros", resultado.Count.ToString() }
            });

        return resultado;
    }

    // MÉTRICA 3: ventas por semana
    public async Task<List<VentasSemanaDto>> VentasPorSemanaAsync(DateTime inicio, int semanas)
    {
        var fin = inicio.AddDays(7 * semanas);

        var ventas = await _ventas
            .Find(v => v.Fecha >= inicio && v.Fecha < fin)
            .ToListAsync();

        var porSemana = ventas
            .GroupBy(v => Math.Min((int)((v.Fecha - inicio).TotalDays / 7), semanas - 1))
            .ToDictionary(g => g.Key, g => g.Sum(v => v.Items.Sum(i => i.Cantidad)));

        var resultado = Enumerable.Range(0, semanas)
            .Select(s => new VentasSemanaDto
            {
                Semana = s + 1,
                Desde = inicio.AddDays(7 * s),
                CantidadTotal = porSemana.GetValueOrDefault(s, 0m)
            })
            .ToList();

        await _log.RegistrarAsync("metrica_ventas_por_semana",
            "Se consultaron las ventas por semana",
            detalle: new Dictionary<string, string>
            {
                { "inicio", inicio.ToString("yyyy-MM-dd") },
                { "semanas", semanas.ToString() },
                { "total", resultado.Sum(r => r.CantidadTotal).ToString() }
            });

        return resultado;
    }
}

public class DetalleLectura
{
    public int IdFactura { get; set; }
    public int IdProducto { get; set; }
    public decimal Cantidad { get; set; }
}

public class NombreLectura
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
}