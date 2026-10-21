using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Inventario
    {
        public int id_inventario { get; set; }
        public int id_producto { get; set; }
        public int stock_actual { get; set; }
        public int stock_minimo { get; set; }
        public int? id_detalle_factura { get; set; }
        public DateTime? fecha_emision { get; set; }
        public int? id_detalle_compra { get; set; }
        public bool estado { get; set; }
    }
}
