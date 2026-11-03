using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Presentacion
    {
        public int id_presentacion { get; set; }
        public string nombre { get; set; }
        public string unidad_medida { get; set; }
        public string peso { get; set; }
        public bool estado { get; set; }
    }
}