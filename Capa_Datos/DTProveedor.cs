using Capa_Entidad;
using Microsoft.Data.SqlClient;

namespace Capa_Datos
{
    public class DTProveedor
    {
        private readonly string _cadenaConexion;

        public DTProveedor(string cadenaConexion)
        {
            _cadenaConexion = !string.IsNullOrWhiteSpace(cadenaConexion)
                ? cadenaConexion
                : throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection.");
        }

        // LISTAR
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Proveedor", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Proveedor()
                    {
                        id_proveedor = Convert.ToInt32(dr["id_proveedor"]),
                        nombre = dr["nombre"].ToString(),
                        correo = dr["correo"].ToString(),
                        telefono = dr["telefono"].ToString(),
                        documento = dr["documento"].ToString(),
                        estado = Convert.ToBoolean(dr["estado"]),
                        fecha_registro = dr["fecha_registro"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(dr["fecha_registro"])
                    });
                }
            }

            return lista;
        }

        // OBTENER
        public Proveedor Obtener(int id)
        {
            Proveedor proveedor = null;

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Proveedor WHERE id_proveedor=@id", cn);

                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    proveedor = new Proveedor()
                    {
                        id_proveedor = Convert.ToInt32(dr["id_proveedor"]),
                        nombre = dr["nombre"].ToString(),
                        correo = dr["correo"].ToString(),
                        telefono = dr["telefono"].ToString(),
                        documento = dr["documento"].ToString(),
                        estado = Convert.ToBoolean(dr["estado"]),
                        fecha_registro = dr["fecha_registro"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(dr["fecha_registro"])
                    };
                }
            }

            return proveedor;
        }

        // INSERTAR
        public bool Insertar(Proveedor obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"INSERT INTO Proveedor
                              (nombre, correo, telefono, documento, estado)
                              VALUES
                              (@nombre, @correo, @telefono, @documento, @estado)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@correo", obj.correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@telefono", obj.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@documento", obj.documento ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ACTUALIZAR
        public bool Actualizar(Proveedor obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"UPDATE Proveedor
                               SET nombre=@nombre,
                                   correo=@correo,
                                   telefono=@telefono,
                                   documento=@documento,
                                   estado=@estado
                               WHERE id_proveedor=@id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", obj.id_proveedor);
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@correo", obj.correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@telefono", obj.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@documento", obj.documento ?? (object)DBNull.Value);
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
                    "DELETE FROM Proveedor WHERE id_proveedor=@id", cn);

                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
