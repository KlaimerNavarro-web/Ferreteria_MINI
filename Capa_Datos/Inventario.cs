using Capa_Entidad;
using Microsoft.Data.SqlClient;
using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class DTInventario
    {
        private readonly string ConnectionString;

        public DTInventario(string connectionString)
        {
            ConnectionString = !string.IsNullOrWhiteSpace(connectionString)
                ? connectionString
                : throw new InvalidOperationException("Falta configurar ConnectionStrings:DefaultConnection.");
        }

        // LISTAR
        public List<Inventario> Listar()
        {
            List<Inventario> lista = new List<Inventario>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Inventario", con);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Inventario()
                    {
                        id_inventario = Convert.ToInt32(dr["id_inventario"]),
                        id_producto = Convert.ToInt32(dr["id_producto"]),
                        stock_actual = Convert.ToInt32(dr["stock_actual"]),
                        stock_minimo = Convert.ToInt32(dr["stock_minimo"]),
                        id_detalle_factura = dr["id_detalle_factura"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["id_detalle_factura"]),
                        fecha_emision = dr["fecha_emision"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["fecha_emision"]),
                        id_detalle_compra = dr["id_detalle_compra"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["id_detalle_compra"]),
                        estado = Convert.ToBoolean(dr["estado"])
                    });
                }
            }

            return lista;
        }

        // OBTENER POR ID
        public Inventario Obtener(int id)
        {
            Inventario inventario = null;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Inventario WHERE id_inventario=@id", con);

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    inventario = new Inventario()
                    {
                        id_inventario = Convert.ToInt32(dr["id_inventario"]),
                        id_producto = Convert.ToInt32(dr["id_producto"]),
                        stock_actual = Convert.ToInt32(dr["stock_actual"]),
                        stock_minimo = Convert.ToInt32(dr["stock_minimo"]),
                        id_detalle_factura = dr["id_detalle_factura"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["id_detalle_factura"]),
                        fecha_emision = dr["fecha_emision"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["fecha_emision"]),
                        id_detalle_compra = dr["id_detalle_compra"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["id_detalle_compra"]),
                        estado = Convert.ToBoolean(dr["estado"])
                    };
                }
            }

            return inventario;
        }

        // INSERTAR
        public bool Insertar(Inventario obj)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sql = @"INSERT INTO Inventario
                            (
                                id_producto,
                                stock_actual,
                                stock_minimo,
                                id_detalle_factura,
                                fecha_emision,
                                id_detalle_compra,
                                estado
                            )
                            VALUES
                            (
                                @id_producto,
                                @stock_actual,
                                @stock_minimo,
                                @id_detalle_factura,
                                @fecha_emision,
                                @id_detalle_compra,
                                @estado
                            )";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id_producto", obj.id_inventario);
                cmd.Parameters.AddWithValue("@stock_actual", obj.stock_actual);
                cmd.Parameters.AddWithValue("@stock_minimo", obj.stock_minimo);
                cmd.Parameters.AddWithValue("@id_detalle_factura", (object)obj.id_detalle_factura ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha_emision", (object)obj.fecha_emision ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id_detalle_compra", (object)obj.id_detalle_compra ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                con.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ACTUALIZAR
        public bool Actualizar(Inventario obj)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string sql = @"UPDATE Inventario
                               SET
                               id_producto=@id_producto,
                               stock_actual=@stock_actual,
                               stock_minimo=@stock_minimo,
                               id_detalle_factura=@id_detalle_factura,
                               fecha_emision=@fecha_emision,
                               id_detalle_compra=@id_detalle_compra,
                               estado=@estado
                               WHERE id_inventario=@id_inventario";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id_inventario", obj.id_inventario);
                cmd.Parameters.AddWithValue("@id_producto", obj.id_producto);
                cmd.Parameters.AddWithValue("@stock_actual", obj.stock_actual);
                cmd.Parameters.AddWithValue("@stock_minimo", obj.stock_minimo);
                cmd.Parameters.AddWithValue("@id_detalle_factura", (object)obj.id_detalle_factura ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha_emision", (object)obj.fecha_emision ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id_detalle_compra", (object)obj.id_detalle_compra ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", obj.estado);

                con.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ELIMINAR
        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Inventario WHERE id_inventario=@id", con);

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
    

