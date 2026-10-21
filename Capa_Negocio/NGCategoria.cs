using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class NGCategoria
    {
        private readonly DTCategoria datos;

        public NGCategoria(string connectionString) => datos = new DTCategoria(connectionString);

        public List<Categoria> Listar()
        {
            return datos.Listar();
        }

        public Categoria Obtener(int id)
        {
            return datos.Obtener(id);
        }

        public bool Insertar(Categoria obj)
        {
            if (string.IsNullOrWhiteSpace(obj.nombre_categoria))
                throw new Exception("El nombre es obligatorio.");

            return datos.Insertar(obj);
        }

        public bool Actualizar(Categoria obj)
        {
            if (obj.id_categoria <= 0)
                throw new Exception("ID inválido.");

            return datos.Actualizar(obj);
        }

        public bool Eliminar(int id)
        {
            return datos.Eliminar(id);
        }
    }
}
