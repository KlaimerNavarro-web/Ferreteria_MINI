using Capa_Entidad;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly NGProducto negocio;

        public ProductoController(IConfiguration configuration)
        {
            negocio = new NGProducto(configuration.GetConnectionString("DefaultConnection")
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
        //    var producto = negocio.Obtener(id);

        //    if (producto == null)
        //        return NotFound();

        //    return Ok(producto);
        //}

        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult Insertar([FromBody] Producto obj)
        {
            bool respuesta = negocio.Insertar(obj);

            if (respuesta)
                return Ok("Producto registrado correctamente");

            return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult Actualizar([FromBody] Producto obj)
        {
            bool respuesta = negocio.Actualizar(obj);

            if (respuesta)
                return Ok("Producto actualizado correctamente");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult Eliminar(int id)
        {
            bool respuesta = negocio.Eliminar(id);

            if (respuesta)
                return Ok("Producto eliminado correctamente");

            return BadRequest();
        }
    }
}
