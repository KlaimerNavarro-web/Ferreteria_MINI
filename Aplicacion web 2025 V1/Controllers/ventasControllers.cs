using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Capa_Negocio;
using Capa_Entidad;

namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ventasControllers : ControllerBase
    {
        private readonly Venta_Negocio _venta_Negocio;
        public ventasControllers(IConfiguration config)
        {
            _venta_Negocio = new Venta_Negocio(config.GetConnectionString("DefaultConnection"));
        }
        [HttpPost]
        [Authorize(Roles ="Admin, empleado")]
        public IActionResult Post([FromBody] Venta2 venta)
        {
            try
            {
                _venta_Negocio.InsertarVenta(venta);
                return Ok("Venta Registrada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

                throw;
            }
        }
        [HttpGet("{VentaId}")]
        public IActionResult Get(int VentaId)
        {
            try
            {
                var venta = _venta_Negocio.ObtenerVentaConDetalle(VentaId);
                if (venta == null || venta.IdVentas == 0)
                    return NotFound("Venta no encontrada.");
                return Ok(venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
