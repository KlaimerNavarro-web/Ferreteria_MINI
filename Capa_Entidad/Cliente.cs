using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Cliente
    {
        public int id_cliente { get; set; }
        public string documento { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public DateTime? fecha_registro { get; set; }
        public bool estado { get; set; }
    }
}