using Capa_Entidad;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentacionController : ControllerBase
    {
        private readonly NGPresentacion negocio;

        public PresentacionController(IConfiguration configuration)
        {
            negocio = new NGPresentacion(configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection."));
        }

        // GET: api/Presentacion
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(negocio.Listar());
        }

        // GET: api/Presentacion/5
        //[HttpGet("{id}")]
        //public IActionResult Obtener(int id)
        //{
        //    var presentacion = negocio.Obtener(id);

        //    if (presentacion == null)
        //        return NotFound();

        //    return Ok(presentacion);
        //}

        // POST: api/Presentacion
        [HttpPost]
        public IActionResult Insertar([FromBody] Presentacion obj)
        {
            bool respuesta = negocio.Insertar(obj);

            if (respuesta)
                return Ok("Presentación registrada correctamente");

            return BadRequest();
        }

        // PUT: api/Presentacion
        [HttpPut]
        public IActionResult Actualizar([FromBody] Presentacion obj)
        {
            bool respuesta = negocio.Actualizar(obj);

            if (respuesta)
                return Ok("Presentación actualizada correctamente");

            return BadRequest();
        }

        // DELETE: api/Presentacion/5
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            bool respuesta = negocio.Eliminar(id);

            if (respuesta)
                return Ok("Presentación eliminada correctamente");

            return BadRequest();
        }
    }
}
