using Capa_Entidad;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly NGProveedor negocio;

        public ProveedorController(IConfiguration configuration)
        {
            negocio = new NGProveedor(configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection."));
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(negocio.Listar());
        }

        //[HttpGet("{id}")]
        //public IActionResult Obtener(int id)
        //{
        //    var proveedor = negocio.Obtener(id);

        //    if (proveedor == null)
        //        return NotFound();

        //    return Ok(proveedor);
        //}

        [HttpPost]
        public IActionResult Insertar([FromBody] Proveedor obj)
        {
            bool respuesta = negocio.Insertar(obj);

            if (respuesta)
                return Ok("Proveedor registrado correctamente");

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Actualizar([FromBody] Proveedor obj)
        {
            bool respuesta = negocio.Actualizar(obj);

            if (respuesta)
                return Ok("Proveedor actualizado correctamente");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            bool respuesta = negocio.Eliminar(id);

            if (respuesta)
                return Ok("Proveedor eliminado correctamente");

            return BadRequest();
        }
    }
}
