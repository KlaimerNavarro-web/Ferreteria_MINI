using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Usuario
    {


       // private string cadenaConexion;

       ///* public Usuario()
       // {
       // }*/

       // public Usuario(string cadenaConexion)
       // {
       //     this.cadenaConexion = cadenaConexion;
       // }



        public int id_usuario { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string contraseña { get; set; } = string.Empty;
        public bool estado { get; set; }
        public string fecha_registro { get; set; } = string.Empty;
        public int oRol { get; set; }
    }
}
