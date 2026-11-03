using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class NGPresentacion
    {
        private readonly DTPresentacion datos;

        public NGPresentacion(string connectionString) => datos = new DTPresentacion(connectionString);

        public List<Presentacion> Listar()
        {
            return datos.Listar();
        }

        public Presentacion Obtener(int id)
        {
            return datos.Obtener(id);
        }

        public bool Insertar(Presentacion obj)
        {
            if (string.IsNullOrWhiteSpace(obj.nombre))
                throw new Exception("El nombre es obligatorio.");

            return datos.Insertar(obj);
        }

        public bool Actualizar(Presentacion obj)
        {
            if (obj.id_presentacion <= 0)
                throw new Exception("ID inválido.");

            return datos.Actualizar(obj);
        }

        public bool Eliminar(int id)
        {
            return datos.Eliminar(id);
        }
    }
}
