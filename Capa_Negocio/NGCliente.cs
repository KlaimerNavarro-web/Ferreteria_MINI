using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class NGCliente
    {
        private readonly DTCliente datos;

        public NGCliente(string connectionString) => datos = new DTCliente(connectionString);

        public List<Cliente> Listar()
        {
            return datos.Listar();
        }

        public Cliente Obtener(int id)
        {
            return datos.Obtener(id);
        }

        public bool Insertar(Cliente obj)
        {
            if (string.IsNullOrWhiteSpace(obj.nombre))
                throw new Exception("El nombre es obligatorio.");

            return datos.Insertar(obj);
        }

        public bool Actualizar(Cliente obj)
        {
            if (obj.id_cliente <= 0)
                throw new Exception("ID inválido.");

            return datos.Actualizar(obj);
        }

        public bool Eliminar(int id)
        {
            return datos.Eliminar(id);
        }
    }
}
