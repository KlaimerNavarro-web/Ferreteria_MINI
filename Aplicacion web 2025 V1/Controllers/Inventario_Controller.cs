using Capa_Entidad;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InventarioController : ControllerBase
{
    private readonly NGInventario negocio;

    public InventarioController(IConfiguration configuration)
    {
        negocio = new NGInventario(configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection."));
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(negocio.Listar());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var inventario = negocio.Obtener(id);

        if (inventario == null)
            return NotFound();

        return Ok(inventario);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Inventario obj)
    {
        return negocio.Insertar(obj)
            ? Ok("Registro insertado correctamente")
            : BadRequest("No se pudo insertar");
    }

    [HttpPut]
    public IActionResult Put([FromBody] Inventario obj)
    {
        return negocio.Actualizar(obj)
            ? Ok("Registro actualizado correctamente")
            : BadRequest("No se pudo actualizar");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return negocio.Eliminar(id)
            ? Ok("Registro eliminado correctamente")
            : BadRequest("No se pudo eliminar");
    }
}

