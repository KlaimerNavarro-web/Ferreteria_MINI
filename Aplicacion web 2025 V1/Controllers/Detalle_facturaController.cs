using Capa_Entidad;
using Capa_Negocio;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Detalle_facturaController : ControllerBase
    {
        private readonly Detalle_Factura_CN _detalleFacturaNegocio;

        public Detalle_facturaController(IConfiguration config)
        {
            _detalleFacturaNegocio = new Detalle_Factura_CN(config.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        /*[Authorize(Roles = "Admin, empleado")]*/
        public IActionResult Post([FromBody] Detalle_Factura detalleFactura)
        {
            try
            {
                _detalleFacturaNegocio.Insertar(detalleFactura);
                return Ok("Detalle de factura registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetDetallesFactura()
        {
            try
            {
                var lista = _detalleFacturaNegocio.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener detalles de factura", error = ex.Message });
            }
        }

        [HttpPut("{id_detalle_factura}")]
        public IActionResult Put(int id_detalle_factura, [FromBody] Detalle_Factura detalleFactura)
        {
            try
            {
                if (id_detalle_factura != detalleFactura.id_detalle_factura)
                    return BadRequest("El Id del detalle de factura no coincide.");

                _detalleFacturaNegocio.Actualizar(detalleFactura);
                return Ok("Detalle de factura actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el detalle de factura", error = ex.Message });
            }
        }

        [HttpDelete("{id_detalle_factura}")]
        public IActionResult Delete(int id_detalle_factura)
        {
            try
            {
                _detalleFacturaNegocio.Eliminar(id_detalle_factura);
                return Ok("Detalle de factura eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el detalle de factura", error = ex.Message });
            }
        }
    }
}