using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Detalle_Compra
    {

        public  int id_detalle_compra { get; set; }
        [Required(ErrorMessage = "El Id de la compra es requerido")]
        public decimal iva  { get; set; }
        public decimal subtotal { get; set; }
        public decimal precio_unitario { get; set; }
        public int cantidad { get; set; } 
        public DateTime fecha_registro { get; set; }

        [Required(ErrorMessage = "El Id de la compra es requerido")]
        public int id_compra { get; set; }
       
        public int id_producto { get; set; }
        
        
    }
}
