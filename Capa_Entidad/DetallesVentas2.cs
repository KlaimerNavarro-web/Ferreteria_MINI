using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class DetallesVentas2
    {
        public int Id_DetalleVenta { get; set; }
        [Required(ErrorMessage = "El Id de la venta es requerido")]
        public int VentaId { get; set; }
        [Required(ErrorMessage = "El Id del Producto es requerido")]
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "El Id de la venta es requerido")]
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; } 
    }
}
