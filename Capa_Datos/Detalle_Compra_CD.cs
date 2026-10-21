using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class Detalle_Compra_CD
    {
        private readonly string ConnectionString;

        public Detalle_Compra_CD(string connectionString)
        {
            ConnectionString = connectionString;
        }

        // ==================================
        // MÉTODO: INSERTAR
        // ==================================
        public bool Insertar(Detalle_Compra detalle)
        {
            bool respuesta = false;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Insertar_Detalle_Compra", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iva", detalle.iva);
                cmd.Parameters.AddWithValue("@subtotal", detalle.subtotal);
                cmd.Parameters.AddWithValue("@precio_unitario", detalle.precio_unitario);
                cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                cmd.Parameters.AddWithValue("@fecha_registro", detalle.fecha_registro);
                cmd.Parameters.AddWithValue("@id_compra", detalle.id_compra);
                cmd.Parameters.AddWithValue("@id_producto", detalle.id_producto);

                try
                {
                    con.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar detalle de compra: " + ex.Message);
                }
            }

            return respuesta;
        }

        // ==================================
        // MÉTODO: LISTAR
        // ==================================
        public List<Detalle_Compra> Listar()
        {
            List<Detalle_Compra> lista = new List<Detalle_Compra>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Listar_Detalle_Compra", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        lista.Add(new Detalle_Compra
                        {
                            id_detalle_compra = Convert.ToInt32(dr["id_detalle_compra"]),
                            iva = Convert.ToDecimal(dr["iva"]),
                            subtotal = Convert.ToDecimal(dr["subtotal"]),
                            precio_unitario = Convert.ToDecimal(dr["precio_unitario"]),
                            cantidad = Convert.ToInt32(dr["cantidad"]),
                            fecha_registro = Convert.ToDateTime(dr["fecha_registro"]),
                            id_compra = Convert.ToInt32(dr["id_compra"]),
                            id_producto = Convert.ToInt32(dr["id_producto"])
                        });
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar detalles de compra: " + ex.Message);
                }
            }

            return lista;
        }

        // ==================================
        // MÉTODO: ACTUALIZAR
        // ==================================
        public bool Actualizar(Detalle_Compra detalle)
        {
            bool respuesta = false;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Actualizar_Detalle_Compra", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_detalle_compra", detalle.id_detalle_compra);
                cmd.Parameters.AddWithValue("@iva", detalle.iva);
                cmd.Parameters.AddWithValue("@subtotal", detalle.subtotal);
                cmd.Parameters.AddWithValue("@precio_unitario", detalle.precio_unitario);
                cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                cmd.Parameters.AddWithValue("@fecha_registro", detalle.fecha_registro);
                cmd.Parameters.AddWithValue("@id_compra", detalle.id_compra);
                cmd.Parameters.AddWithValue("@id_producto", detalle.id_producto);

                try
                {
                    con.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar detalle de compra: " + ex.Message);
                }
            }

            return respuesta;
        }

        // ==================================
        // MÉTODO: ELIMINAR
        // ==================================
        public bool Eliminar(int id_detalle_compra)
        {
            bool respuesta = false;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Eliminar_Detalle_Compra", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_detalle_compra", id_detalle_compra);

                try
                {
                    con.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar detalle de compra: " + ex.Message);
                }
            }

            return respuesta;
        }

        // ==================================
        // MÉTODO: OBTENER POR ID
        // ==================================
        public Detalle_Compra? ObtenerPorId(int id_detalle_compra)
        {
            Detalle_Compra? detalle = null;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Obtener_Detalle_Compra_PorId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_detalle_compra", id_detalle_compra);

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        detalle = new Detalle_Compra
                        {
                            id_detalle_compra = dr["id_detalle_compra"] != DBNull.Value ? Convert.ToInt32(dr["id_detalle_compra"]) : 0,
                            iva = dr["iva"] != DBNull.Value ? Convert.ToDecimal(dr["iva"]) : 0,
                            subtotal = dr["subtotal"] != DBNull.Value ? Convert.ToDecimal(dr["subtotal"]) : 0,
                            precio_unitario = dr["precio_unitario"] != DBNull.Value ? Convert.ToDecimal(dr["precio_unitario"]) : 0,
                            cantidad = dr["cantidad"] != DBNull.Value ? Convert.ToInt32(dr["cantidad"]) : 0,
                            fecha_registro = dr["fecha_registro"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_registro"]) : DateTime.MinValue,
                            id_compra = dr["id_compra"] != DBNull.Value ? Convert.ToInt32(dr["id_compra"]) : 0,
                            id_producto = dr["id_producto"] != DBNull.Value ? Convert.ToInt32(dr["id_producto"]) : 0
                        };
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener detalle de compra: " + ex.Message);
                }
            }

            return detalle;
        }

    }
}
