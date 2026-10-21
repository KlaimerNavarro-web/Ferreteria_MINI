using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class NGProducto
    {
        private readonly DTProducto datos;

        public NGProducto(string connectionString) => datos = new DTProducto(connectionString);

        public List<Producto> Listar()
        {
            return datos.Listar();
        }

        public Producto Obtener(int id)
        {
            return datos.Obtener(id);
        }

        public bool Insertar(Producto obj)
        {
            if (string.IsNullOrWhiteSpace(obj.nombre))
                throw new Exception("El nombre es obligatorio.");

            if (obj.id_categoria <= 0)
                throw new Exception("Debe seleccionar una categoría.");

            if (obj.id_presentacion <= 0)
                throw new Exception("Debe seleccionar una presentación.");

            return datos.Insertar(obj);
        }

        public bool Actualizar(Producto obj)
        {
            if (obj.id_producto <= 0)
                throw new Exception("ID inválido.");

            return datos.Actualizar(obj);
        }

        public bool Eliminar(int id)
        {
            return datos.Eliminar(id);
        }
    }
}
