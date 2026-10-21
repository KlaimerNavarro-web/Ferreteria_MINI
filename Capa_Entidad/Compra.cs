using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public  class Compra
    {
        public int id_compra { get; set; }

        public int id_usuario { get; set; }

        public int id_proveedor { get; set; }
        public string numero_documento { get; set; }

        public decimal total { get; set; }

        public DateTime fecha_compra { get; set; }

        public List<Detalle_Compra> Detalle_Compra { get; set; } = new List<Detalle_Compra>();

    }
}
