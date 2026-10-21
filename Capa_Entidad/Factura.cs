using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Factura
    {
        public int id_factura { get; set; }
        public DateTime fecha_venta { get; set; }
        public Decimal monto_pago { get; set; }
        public Decimal monto_cambio { get; set; }
        public int numero_comprobante { get; set; }
        public int id_cliente { get; set; }
        public int id_usuario { get; set; }
        public Decimal descuento { get; set; }
        public Decimal total { get; set; }

        // Lista de detalles de la factura
        public List<Detalle_Factura> Detalle_Factura { get; set; } = new List<Detalle_Factura>();
    }
}
