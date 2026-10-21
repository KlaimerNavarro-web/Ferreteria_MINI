using Capa_Entidad;
using Microsoft.Data.SqlClient;

namespace Capa_Datos
{
    public class DTCategoria
    {
        private readonly string _cadenaConexion;

        public DTCategoria(string cadenaConexion)
        {
            _cadenaConexion = !string.IsNullOrWhiteSpace(cadenaConexion)
                ? cadenaConexion
                : throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection.");
        }

        // LISTAR
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Categoria", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Categoria()
                    {
                        id_categoria = Convert.ToInt32(dr["id_categoria"]),
                        nombre_categoria = dr["nombre_categoria"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        estado = Convert.ToBoolean(dr["estado"])
                    });
                }
            }

            return lista;
        }

        // OBTENER
        public Categoria Obtener(int id)
        {
            Categoria categoria = null;

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Categoria WHERE id_categoria=@id_categoria", cn);

                cmd.Parameters.AddWithValue("@id_categoria", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    categoria = new Categoria()
                    {
                        id_categoria = Convert.ToInt32(dr["id_categoria"]),
                        nombre_categoria = dr["nombre_categoria"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        estado = Convert.ToBoolean(dr["estado"])
                    };
                }
            }

            return categoria;
        }

        // INSERTAR
        public bool Insertar(Categoria obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"INSERT INTO Categoria
                              (nombre_categoria, descripcion, estado)
                              VALUES
                              (@nombre_categoria, @descripcion, @estado)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre_categoria", obj.nombre_categoria);
                cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ACTUALIZAR
        public bool Actualizar(Categoria obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"UPDATE Categoria
                               SET nombre_categoria=@nombre_categoria,
                                   descripcion=@descripcion,
                                   estado=@estado
                               WHERE id_categoria=@id_categoria";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_categoria", obj.id_categoria);
                cmd.Parameters.AddWithValue("@nombre_categoria", obj.nombre_categoria);
                cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ELIMINAR
        public bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Categoria WHERE id_categoria=@id_categoria", cn);

                cmd.Parameters.AddWithValue("@id_categoria", id);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
