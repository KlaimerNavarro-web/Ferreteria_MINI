using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Detalle_Factura
    {

        public int id_detalle_factura { get; set; }
        public int id_producto { get; set; }
        public int id_factura { get; set; }
        public int precio_venta { get; set; }
        public Decimal cantidad { get; set; }
        public Decimal iva { get; set; }
        public DateTime fecha_registro { get; set; }

        //public List<Detalle_Factura> detalleFactura { get; set; }
 
    }
}
