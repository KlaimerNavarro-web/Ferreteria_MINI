using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class Detalle_Factura_CD
    {
        private readonly string ConnectionString;

        public Detalle_Factura_CD(string connectionString)
        {
            ConnectionString = connectionString;
        }

        
        // MÉTODO: INSERTAR
        
        public bool Insertar(Detalle_Factura detalle)
        {
            bool respuesta = false;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Insertar_Detalle_Factura", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.AddWithValue("@id_producto", detalle.id_producto);
                cmd.Parameters.AddWithValue("@id_factura", detalle.id_factura);
                cmd.Parameters.AddWithValue("@precio_venta", detalle.precio_venta);
                cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                cmd.Parameters.AddWithValue("@iva", detalle.iva);
                cmd.Parameters.AddWithValue("@fecha_registro", detalle.fecha_registro);

                try
                {
                    con.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar detalle de factura: " + ex.Message);
                }
            }

            return respuesta;
        }
        
        
        // MÉTODO: LISTAR
        
        public List<Detalle_Factura> Listar()
        {
            List<Detalle_Factura> lista = new List<Detalle_Factura>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Listar_Detalle_Factura", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        lista.Add(new Detalle_Factura
                        {
                            id_detalle_factura = Convert.ToInt32(dr["id_detalle_factura"]),
                            id_producto = Convert.ToInt32(dr["id_producto"]),
                            id_factura = Convert.ToInt32(dr["id_factura"]),
                            precio_venta = Convert.ToInt32(dr["precio_venta"]),
                            cantidad = Convert.ToDecimal(dr["cantidad"]),
                            iva = Convert.ToDecimal(dr["iva"]),
                            fecha_registro = Convert.ToDateTime(dr["fecha_registro"])
                        });
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar detalles de factura: " + ex.Message);
                }
            }

            return lista;
        }

        
        // MÉTODO: ACTUALIZAR
        
        public bool Actualizar(Detalle_Factura detalle)
        {
            bool respuesta = false;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Actualizar_Detalle_Factura", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_detalle_factura", detalle.id_detalle_factura);
                cmd.Parameters.AddWithValue("@id_producto", detalle.id_producto);
                cmd.Parameters.AddWithValue("@id_factura", detalle.id_factura);
                cmd.Parameters.AddWithValue("@precio_venta", detalle.precio_venta);
                cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                cmd.Parameters.AddWithValue("@iva", detalle.iva);
                cmd.Parameters.AddWithValue("@fecha_registro", detalle.fecha_registro);

                try
                {
                    con.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar detalle de factura: " + ex.Message);
                }
            }

            return respuesta;
        }

        
        // MÉTODO: ELIMINAR
        
        public bool Eliminar(int id_detalle_factura)
        {
            bool respuesta = false;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Eliminar_Detalle_Factura", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_detalle_factura", id_detalle_factura);

                try
                {
                    con.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar detalle de factura: " + ex.Message);
                }
            }

            return respuesta;
        }

        
        // MÉTODO: OBTENER POR ID
       
        public Detalle_Factura? ObtenerPorId(int id_detalle_factura)
        {
            Detalle_Factura? detalle = null;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_Obtener_Detalle_Factura_PorId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_detalle_factura", id_detalle_factura);

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        detalle = new Detalle_Factura
                        {
                            id_detalle_factura = dr["id_detalle_factura"] != DBNull.Value ? Convert.ToInt32(dr["id_detalle_factura"]) : 0,
                            id_producto = dr["id_producto"] != DBNull.Value ? Convert.ToInt32(dr["id_producto"]) : 0,
                            id_factura = dr["id_factura"] != DBNull.Value ? Convert.ToInt32(dr["id_factura"]) : 0,
                            precio_venta = dr["precio_venta"] != DBNull.Value ? Convert.ToInt32(dr["precio_venta"]) : 0,
                            cantidad = dr["cantidad"] != DBNull.Value ? Convert.ToDecimal(dr["cantidad"]) : 0,
                            iva = dr["iva"] != DBNull.Value ? Convert.ToDecimal(dr["iva"]) : 0,
                            fecha_registro = dr["fecha_registro"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_registro"]) : DateTime.MinValue
                        };
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener detalle de factura: " + ex.Message);
                }
            }

            return detalle;
        }
    }
}
