using Capa_Entidad;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly NGCategoria negocio;

        public CategoriaController(IConfiguration configuration)
        {
            negocio = new NGCategoria(configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection."));
        }

        // GET: api/categoria
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(negocio.Listar());
        }

        // GET: api/categoria/5
        //[HttpGet("{id}")]
        //public IActionResult Obtener(int id)
        //{
        //    var categoria = negocio.Obtener(id);

        //    if (categoria == null)
        //        return NotFound();

        //    return Ok(categoria);
        //}

        // POST: api/categoria
        [HttpPost]
        public IActionResult Insertar([FromBody] Categoria obj)
        {
            bool respuesta = negocio.Insertar(obj);

            if (respuesta)
                return Ok("Categoría registrada correctamente");

            return BadRequest();
        }

        // PUT: api/categoria
        [HttpPut]
        public IActionResult Actualizar([FromBody] Categoria obj)
        {
            bool respuesta = negocio.Actualizar(obj);

            if (respuesta)
                return Ok("Categoría actualizada correctamente");

            return BadRequest();
        }

        // DELETE: api/categoria/5
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            bool respuesta = negocio.Eliminar(id);

            if (respuesta)
                return Ok("Categoría eliminada correctamente");

            return BadRequest();
        }
    }
}
