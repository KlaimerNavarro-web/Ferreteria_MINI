using Aplicacion_web_2025_V1.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/metricas")]
[Authorize(Roles = "ADMINISTRADOR")]
public class MetricasController : ControllerBase
{
    private readonly MetricasService _service;

    public MetricasController(MetricasService service) => _service = service;

    [HttpGet("ventas/promedio-producto-cliente")]
    public async Task<IActionResult> PromedioProductoCliente(
        [FromQuery] DateTime inicio, [FromQuery] int semanas = 3)
    {
        if (semanas < 1 || semanas > 12)
            return BadRequest("El parámetro 'semanas' debe estar entre 1 y 12.");

        var datos = await _service.PromedioProductoClienteAsync(inicio, semanas);
        return Ok(new { periodo = $"{semanas} semanas", datos });
    }

 
    [HttpGet("productos-mas-vendidos")]
    public async Task<IActionResult> ProductosMasVendidos([FromQuery] int limite = 5)
    {
        if (limite < 1 || limite > 50)
            return BadRequest("El parámetro 'limite' debe estar entre 1 y 50.");

        var datos = await _service.ProductosMasVendidosAsync(limite);
        return Ok(datos);
    }

    [HttpGet("ventas-por-semana")]
    public async Task<IActionResult> VentasPorSemana(
        [FromQuery] DateTime inicio, [FromQuery] int semanas = 4)
    {
        if (semanas < 1 || semanas > 12)
            return BadRequest("El parámetro 'semanas' debe estar entre 1 y 12.");

        var datos = await _service.VentasPorSemanaAsync(inicio, semanas);
        return Ok(new { periodo = $"{semanas} semanas", datos });
    }

    [HttpPost("sincronizar")]
    public async Task<IActionResult> Sincronizar([FromServices] AppDbContext db)
    {
        var total = await _service.SincronizarAsync(db);
        return Ok(new { mensaje = "Sincronización completada", facturas = total });
    }
}