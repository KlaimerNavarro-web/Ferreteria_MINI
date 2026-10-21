using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class Factura_CD
    {
        private readonly string ConnectionString;

        public Factura_CD(string connectionString)
        {
            ConnectionString = connectionString;
        }

        // Listar todas las facturas
        public List<Factura> Listar()
        {
            List<Factura> lista = new List<Factura>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    string query = "SELECT * FROM Factura"; 
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Factura()
                            {
                                id_factura = Convert.ToInt32(dr["id_factura"]),
                                fecha_venta = Convert.ToDateTime(dr["fecha_venta"]),
                                monto_pago = Convert.ToDecimal(dr["monto_pago"]),
                                monto_cambio = Convert.ToDecimal(dr["monto_cambio"]),
                                numero_comprobante = Convert.ToInt32(dr["numero_comprobante"]),
                                id_cliente = Convert.ToInt32(dr["id_cliente"]),
                                id_usuario = Convert.ToInt32(dr["id_usuario"]),
                                descuento = Convert.ToDecimal(dr["descuento"]),
                                total = Convert.ToDecimal(dr["total"])
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Factura>();
                }
            }

            return lista;
        }

        // Insertar una factura
        public void InsertarFactura(Factura factura)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // Insertar la factura y obtener el ID generado
                    SqlCommand cmd = new SqlCommand("sp_InsertarFactura", con, tran);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fecha_venta", factura.fecha_venta);
                    cmd.Parameters.AddWithValue("@monto_pago", factura.monto_pago);
                    cmd.Parameters.AddWithValue("@monto_cambio", factura.monto_cambio);
                    cmd.Parameters.AddWithValue("@numero_comprobante", factura.numero_comprobante);
                    cmd.Parameters.AddWithValue("@id_cliente", factura.id_cliente);
                    cmd.Parameters.AddWithValue("@id_usuario", factura.id_usuario);
                    cmd.Parameters.AddWithValue("@descuento", factura.descuento);
                    cmd.Parameters.AddWithValue("@total", factura.total);

                    int facturaId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Insertar cada detalle de la factura
                    foreach (Detalle_Factura detalle in factura.Detalle_Factura)
                    {
                        SqlCommand cmdDetalles = new SqlCommand("sP_InsertarDetalleFactura", con, tran);
                        cmdDetalles.CommandType = CommandType.StoredProcedure;

                        cmdDetalles.Parameters.AddWithValue("@id_producto", detalle.id_producto);
                        cmdDetalles.Parameters.AddWithValue("@precio_venta", detalle.precio_venta);
                        cmdDetalles.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                        cmdDetalles.Parameters.AddWithValue("@iva", detalle.iva);
                        cmdDetalles.Parameters.AddWithValue("@id_factura", facturaId); // Usar el ID de la factura recién insertada

                        cmdDetalles.ExecuteNonQuery(); // Ejecutar el comando de detalle
                    }

                    tran.Commit(); // Confirmar la transacción
                }
                catch (Exception ex)
                {
                    tran.Rollback(); // Revertir la transacción en caso de error
                    throw new Exception("Error al insertar la factura: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }




        // Seleccionar factura por Id
        public Factura SeleccionarPorId(int id_factura)
        {
            Factura factura = null;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SeleccionarFacturaPorId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_factura", id_factura);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        factura = new Factura()
                        {
                            id_factura = Convert.ToInt32(dr["id_factura"]),
                            fecha_venta = Convert.ToDateTime(dr["fecha_venta"]),
                            monto_pago = Convert.ToDecimal(dr["monto_pago"]),
                            monto_cambio = Convert.ToDecimal(dr["monto_cambio"]),
                            numero_comprobante = Convert.ToInt32(dr["numero_comprobante"]),
                            id_cliente = Convert.ToInt32(dr["id_cliente"]),
                            id_usuario = Convert.ToInt32(dr["id_usuario"]),
                            descuento = Convert.ToDecimal(dr["descuento"]),
                            total = Convert.ToDecimal(dr["total"])
                        };
                    }
                }
            }

            return factura;
        }

        // Actualizar factura
        public void ActualizarFactura(Factura factura)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarFactura", con, tran); 
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_factura", factura.id_factura);
                    cmd.Parameters.AddWithValue("@fecha_venta", factura.fecha_venta);
                    cmd.Parameters.AddWithValue("@monto_pago", factura.monto_pago);
                    cmd.Parameters.AddWithValue("@monto_cambio", factura.monto_cambio);
                    cmd.Parameters.AddWithValue("@numero_comprobante", factura.numero_comprobante);
                    cmd.Parameters.AddWithValue("@id_cliente", factura.id_cliente);
                    cmd.Parameters.AddWithValue("@id_usuario", factura.id_usuario);
                    cmd.Parameters.AddWithValue("@descuento", factura.descuento);
                    cmd.Parameters.AddWithValue("@total", factura.total);

                    cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw new Exception("Error al actualizar la factura");
                }
                finally
                {
                    con.Close();
                }
            }
        }

        // Eliminar factura
        public void EliminarFactura(int id_factura)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarFactura", con, tran); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_factura", id_factura);

                    cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw new Exception("Error al eliminar la factura");
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
