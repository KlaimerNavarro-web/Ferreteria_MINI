using Capa_Entidad;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Factura_Controller : ControllerBase
    {
        private readonly Factura_Negocio _facturaNegocio;

        public Factura_Controller(IConfiguration config)
        {
            _facturaNegocio = new Factura_Negocio(config.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        /*[Authorize(Roles = "Admin, empleado")]*/
        public IActionResult Post([FromBody] Factura factura)
        {
            try
            {
                _facturaNegocio.InsertarFactura(factura);
                return Ok("Factura registrada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetFacturas()
        {
            try
            {
                var lista = _facturaNegocio.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener facturas", error = ex.Message });
            }
        }

      /*  [HttpGet("{id_factura}")]
        public IActionResult GetFactura(int id_factura)
        {
            try
            {
                if (id_factura <= 0)
                    return BadRequest("El Id de la factura no es válido.");

                var factura = _facturaNegocio.ObtenerFacturaConDetalle(id_factura);
                if (factura == null)
                    return NotFound("Factura no encontrada.");

                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la factura", error = ex.Message });
            }
        }*/

        [HttpPut("{id_factura}")]
        public IActionResult Put(int id_factura, [FromBody] Factura factura)
        {
            try
            {
                if (id_factura != factura.id_factura)
                    return BadRequest("El Id de la factura no coincide.");

                _facturaNegocio.ActualizarFactura(factura);
                return Ok("Factura actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar la factura", error = ex.Message });
            }
        }

        [HttpDelete("{id_factura}")]
        public IActionResult Delete(int id_factura)
        {
            try
            {
                _facturaNegocio.EliminarFactura(id_factura);
                return Ok("Factura eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar la factura", error = ex.Message });
            }
        }
    }
}

