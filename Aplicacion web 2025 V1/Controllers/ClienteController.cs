using Capa_Entidad;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly NGCliente negocio;

        public ClienteController(IConfiguration configuration)
        {
            negocio = new NGCliente(configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection."));
        }

        // GET: api/Cliente
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(negocio.Listar());
        }

        // GET: api/Cliente/5
        //[HttpGet("{id}")]
        //public IActionResult Obtener(int id)
        //{
        //    var cliente = negocio.Obtener(id);

        //    if (cliente == null)
        //        return NotFound();

        //    return Ok(cliente);
        //}

        // POST: api/Cliente
        [HttpPost]
        public IActionResult Insertar([FromBody] Cliente obj)
        {
            bool respuesta = negocio.Insertar(obj);

            if (respuesta)
                return Ok("Cliente registrado correctamente");

            return BadRequest();
        }

        // PUT: api/Cliente
        [HttpPut]
        public IActionResult Actualizar([FromBody] Cliente obj)
        {
            bool respuesta = negocio.Actualizar(obj);

            if (respuesta)
                return Ok("Cliente actualizado correctamente");

            return BadRequest();
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            bool respuesta = negocio.Eliminar(id);

            if (respuesta)
                return Ok("Cliente eliminado correctamente");

            return BadRequest();
        }
    }
}
