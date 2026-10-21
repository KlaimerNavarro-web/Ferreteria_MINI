using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Venta2
    {
        public int IdVentas { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }

        public List<DetallesVentas2> DetallesVentas { get; set; }
    }
}
