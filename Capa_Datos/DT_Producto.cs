using Capa_Entidad;
using Microsoft.Data.SqlClient;

namespace Capa_Datos
{
    public class DTProducto
    {
        private readonly string _cadenaConexion;

        public DTProducto(string cadenaConexion)
        {
            _cadenaConexion = !string.IsNullOrWhiteSpace(cadenaConexion)
                ? cadenaConexion
                : throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection.");
        }

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Producto", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Producto()
                    {
                        id_producto = Convert.ToInt32(dr["id_producto"]),
                        nombre = dr["nombre"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        id_categoria = Convert.ToInt32(dr["id_categoria"]),
                        id_presentacion = Convert.ToInt32(dr["id_presentacion"]),
                        estado = Convert.ToBoolean(dr["estado"]),
                        fecha_registro = dr["fecha_registro"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(dr["fecha_registro"])
                    });
                }
            }

            return lista;
        }

        public Producto Obtener(int id)
        {
            Producto producto = null;

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Producto WHERE id_producto=@id_producto", cn);

                cmd.Parameters.AddWithValue("@id_producto", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    producto = new Producto()
                    {
                        id_producto = Convert.ToInt32(dr["id_producto"]),
                        nombre = dr["nombre"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        id_categoria = Convert.ToInt32(dr["id_categoria"]),
                        id_presentacion = Convert.ToInt32(dr["id_presentacion"]),
                        estado = Convert.ToBoolean(dr["estado"]),
                        fecha_registro = dr["fecha_registro"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(dr["fecha_registro"])
                    };
                }
            }

            return producto;
        }

        public bool Insertar(Producto obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"INSERT INTO Producto
                              (
                                nombre,
                                descripcion,
                                id_categoria,
                                id_presentacion,
                                estado
                              )
                              VALUES
                              (
                                @nombre,
                                @descripcion,
                                @id_categoria,
                                @id_presentacion,
                                @estado
                              )";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                cmd.Parameters.AddWithValue("@id_categoria", obj.id_categoria);
                cmd.Parameters.AddWithValue("@id_presentacion", obj.id_presentacion);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Producto obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"UPDATE Producto
                               SET
                               nombre=@nombre,
                               descripcion=@descripcion,
                               id_categoria=@id_categoria,
                               id_presentacion=@id_presentacion,
                               estado=@estado
                               WHERE id_producto=@id_producto";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_producto", obj.id_producto);
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                cmd.Parameters.AddWithValue("@id_categoria", obj.id_categoria);
                cmd.Parameters.AddWithValue("@id_presentacion", obj.id_presentacion);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Producto WHERE id_producto=@id_producto", cn);

                cmd.Parameters.AddWithValue("@id_producto", id);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
