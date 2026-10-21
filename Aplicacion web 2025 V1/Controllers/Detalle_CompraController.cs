using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Detalle_CompraController : ControllerBase
    {
        private readonly Detalle_CompraCN _detalleCompraNegocio;

        public Detalle_CompraController(IConfiguration config)
        {
            _detalleCompraNegocio = new Detalle_CompraCN(config.GetConnectionString("DefaultConnection"));
        }


        //      INSERTAR

        [HttpPost]
        /*[Authorize(Roles = "Admin, empleado")]*/
        public IActionResult Post([FromBody] Detalle_Compra detalle)
        {
            try
            {
                _detalleCompraNegocio.Insertar(detalle);
                return Ok("Detalle de compra registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = "Error al registrar el detalle de compra", error = ex.Message });
            }
        }

        //      LISTAR
   
        [HttpGet]
        public IActionResult GetDetallesCompra()
        {
            try
            {
                var lista = _detalleCompraNegocio.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener detalles de compra", error = ex.Message });
            }
        }

        //      ACTUALIZAR

        [HttpPut("{id_detalle_compra}")]
        public IActionResult Put(int id_detalle_compra, [FromBody] Detalle_Compra detalle)
        {
            try
            {
                if (id_detalle_compra != detalle.id_detalle_compra)
                    return BadRequest("El Id del detalle de compra no coincide.");

                _detalleCompraNegocio.Actualizar(detalle);
                return Ok("Detalle de compra actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el detalle de compra", error = ex.Message });
            }
        }


        //      ELIMINAR

        [HttpDelete("{id_detalle_compra}")]
        public IActionResult Delete(int id_detalle_compra)
        {
            try
            {
                _detalleCompraNegocio.Eliminar(id_detalle_compra);
                return Ok("Detalle de compra eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el detalle de compra", error = ex.Message });
            }
        }
    }
}