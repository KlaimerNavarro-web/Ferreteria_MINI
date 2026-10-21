using Capa_Entidad;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class compraController : ControllerBase
    {
        private readonly Compra_Negocio _compraNegocio;

        public compraController(IConfiguration config)
        {
            _compraNegocio = new Compra_Negocio(config.GetConnectionString("DefaultConnection"));
        }
        [HttpPost]
        /*[Authorize(Roles = "Admin, empleado")]*/
        public IActionResult Post([FromBody] Compra compra)
        {
            try
            {
                _compraNegocio.InsertarCompra(compra);
                return Ok("Compra registrada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpGet]
        public IActionResult GetCompra()
        {
            try
            {
                var lista = _compraNegocio.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener compras", error = ex.Message });
            }
        }
        [HttpPut("{id_compra}")]
        public IActionResult Put(int id_compra, [FromBody] Compra compra)
        {
            try
            {
                if (id_compra != compra.id_compra)
                    return BadRequest("El Id de la compra no coincide.");

                _compraNegocio.ActualizarCompra(compra);
                return Ok("Compra actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar la compra", error = ex.Message });
            }
        }
        [HttpDelete("{id_compra}")]
        public IActionResult Delete(int id_compra)
        {
            try
            {
                _compraNegocio.EliminarCompra(id_compra);
                return Ok("Compra eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar la compra", error = ex.Message });
            }
        }
    }
}
