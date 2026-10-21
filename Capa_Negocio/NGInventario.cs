using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class NGInventario
    {
        private readonly DTInventario datos;

        public NGInventario(string connectionString) => datos = new DTInventario(connectionString);

        public List<Inventario> Listar()
        {
            return datos.Listar();
        }

        public Inventario Obtener(int id)
        {
            return datos.Obtener(id);
        }

        public bool Insertar(Inventario obj)
        {
            return datos.Insertar(obj);
        }

        public bool Actualizar(Inventario obj)
        {
            return datos.Actualizar(obj);
        }

        public bool Eliminar(int id)
        {
            return datos.Eliminar(id);
        }
    }
}
