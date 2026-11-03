using Capa_Entidad;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class DTCliente
    {
        private readonly string _cadenaConexion;

        public DTCliente(string cadenaConexion)
        {
            _cadenaConexion = !string.IsNullOrWhiteSpace(cadenaConexion)
                ? cadenaConexion
                : throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection.");
        }

        // LISTAR
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cliente()
                    {
                        id_cliente = Convert.ToInt32(dr["id_cliente"]),
                        documento = dr["documento"].ToString().Trim(),
                        nombre = dr["nombre"].ToString(),
                        correo = dr["correo"].ToString(),
                        telefono = dr["telefono"].ToString(),
                        fecha_registro = dr["fecha_registro"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(dr["fecha_registro"]),
                        estado = Convert.ToBoolean(dr["estado"])
                    });
                }
            }

            return lista;
        }

        // OBTENER
        public Cliente Obtener(int id)
        {
            Cliente cliente = null;

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Cliente WHERE id_cliente=@id", cn);

                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cliente = new Cliente()
                    {
                        id_cliente = Convert.ToInt32(dr["id_cliente"]),
                        documento = dr["documento"].ToString().Trim(),
                        nombre = dr["nombre"].ToString(),
                        correo = dr["correo"].ToString(),
                        telefono = dr["telefono"].ToString(),
                        fecha_registro = dr["fecha_registro"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(dr["fecha_registro"]),
                        estado = Convert.ToBoolean(dr["estado"])
                    };
                }
            }

            return cliente;
        }

        // INSERTAR
        public bool Insertar(Cliente obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"INSERT INTO Cliente
                               (
                                   documento,
                                   nombre,
                                   correo,
                                   telefono,
                                   estado
                               )
                               VALUES
                               (
                                   @documento,
                                   @nombre,
                                   @correo,
                                   @telefono,
                                   @estado
                               )";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@documento", (object)obj.documento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@correo", (object)obj.correo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@telefono", (object)obj.telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ACTUALIZAR
        public bool Actualizar(Cliente obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"UPDATE Cliente
                               SET
                               documento=@documento,
                               nombre=@nombre,
                               correo=@correo,
                               telefono=@telefono,
                               estado=@estado
                               WHERE id_cliente=@id_cliente";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_cliente", obj.id_cliente);
                cmd.Parameters.AddWithValue("@documento", (object)obj.documento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@correo", (object)obj.correo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@telefono", (object)obj.telefono ?? DBNull.Value);
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
                    "DELETE FROM Cliente WHERE id_cliente=@id", cn);

                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
