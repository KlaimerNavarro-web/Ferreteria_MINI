using Microsoft.AspNetCore.Mvc;
using Capa_Negocio;
using Capa_Entidad;

[ApiController]
[Route("api/[controller]")]
public class ListaFacturaController : ControllerBase
{
    private readonly Detalle_Factura_CN _detallefacturaNegocio;

    public ListaFacturaController(IConfiguration config)
    {
        _detallefacturaNegocio = new Detalle_Factura_CN(
            config.GetConnectionString("DefaultConnection")
        );
    }

    // GET: api/ListaFactura
    [HttpGet]
    public IActionResult ListarDetallesFactura()
    {
        try
        {
            var lista = _detallefacturaNegocio.Listar();

            if (lista == null || lista.Count == 0)
                return NotFound("No hay detalles de factura registrados.");

            return Ok(lista);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error en el servidor: " + ex.Message);
        }
    }
}


