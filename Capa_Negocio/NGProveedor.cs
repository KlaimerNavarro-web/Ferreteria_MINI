using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class NGProveedor
    {
        private readonly DTProveedor datos;

        public NGProveedor(string connectionString) => datos = new DTProveedor(connectionString);

        public List<Proveedor> Listar()
        {
            return datos.Listar();
        }

        public Proveedor Obtener(int id)
        {
            return datos.Obtener(id);
        }

        public bool Insertar(Proveedor obj)
        {
            if (string.IsNullOrWhiteSpace(obj.nombre))
                throw new Exception("El nombre es obligatorio.");

            return datos.Insertar(obj);
        }

        public bool Actualizar(Proveedor obj)
        {
            if (obj.id_proveedor <= 0)
                throw new Exception("ID inválido.");

            return datos.Actualizar(obj);
        }

        public bool Eliminar(int id)
        {
            return datos.Eliminar(id);
        }
    }
}
