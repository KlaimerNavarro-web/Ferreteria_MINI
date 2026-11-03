using Capa_Entidad;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class DTPresentacion
    {
        private readonly string _cadenaConexion;

        public DTPresentacion(string cadenaConexion)
        {
            _cadenaConexion = !string.IsNullOrWhiteSpace(cadenaConexion)
                ? cadenaConexion
                : throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection.");
        }

        // LISTAR
        public List<Presentacion> Listar()
        {
            List<Presentacion> lista = new List<Presentacion>();

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Presentacion", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Presentacion()
                    {
                        id_presentacion = Convert.ToInt32(dr["id_presentacion"]),
                        nombre = dr["nombre"].ToString(),
                        unidad_medida = dr["unidad_medida"].ToString(),
                        peso = dr["peso"].ToString(),
                        estado = Convert.ToBoolean(dr["estado"])
                    });
                }
            }

            return lista;
        }

        // OBTENER
        public Presentacion Obtener(int id)
        {
            Presentacion presentacion = null;

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Presentacion WHERE id_presentacion=@id", cn);

                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    presentacion = new Presentacion()
                    {
                        id_presentacion = Convert.ToInt32(dr["id_presentacion"]),
                        nombre = dr["nombre"].ToString(),
                        unidad_medida = dr["unidad_medida"].ToString(),
                        peso = dr["peso"].ToString(),
                        estado = Convert.ToBoolean(dr["estado"])
                    };
                }
            }

            return presentacion;
        }

        // INSERTAR
        public bool Insertar(Presentacion obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"INSERT INTO Presentacion
                              (
                                nombre,
                                unidad_medida,
                                peso,
                                estado
                              )
                              VALUES
                              (
                                @nombre,
                                @unidad_medida,
                                @peso,
                                @estado
                              )";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@unidad_medida", obj.unidad_medida);
                cmd.Parameters.AddWithValue("@peso", obj.peso);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ACTUALIZAR
        public bool Actualizar(Presentacion obj)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                string sql = @"UPDATE Presentacion
                               SET
                               nombre=@nombre,
                               unidad_medida=@unidad_medida,
                               peso=@peso,
                               estado=@estado
                               WHERE id_presentacion=@id_presentacion";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_presentacion", obj.id_presentacion);
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@unidad_medida", obj.unidad_medida);
                cmd.Parameters.AddWithValue("@peso", obj.peso);
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
                    "DELETE FROM Presentacion WHERE id_presentacion=@id", cn);

                cmd.Parameters.AddWithValue("@id", id);

                cn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
