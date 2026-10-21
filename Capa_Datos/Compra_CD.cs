using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Capa_Entidad;
using Microsoft.Extensions.Logging;

namespace Capa_Datos
{
    public class Compra_CD
    {
        private readonly string ConnectionString;
        private static List<Compra> compras = new List<Compra>();

        public Compra_CD(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Compra> Listar()
        {
            List<Compra> lista = new List<Compra>();

            using (SqlConnection oconexion = new SqlConnection(ConnectionString))
            {
                try
                {
                    // Consulta SQL para obtener las compras y su proveedor
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT c.id_compra, c.fecha_compra, c.total, c.id_proveedor");
                    query.AppendLine("FROM Compra c");
                    query.AppendLine("INNER JOIN Proveedor p ON p.id_proveedor = c.id_proveedor");

                    // Ejecutar el comando
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Compra()
                            {
                                id_compra = Convert.ToInt32(dr["id_compra"]),
                                fecha_compra = Convert.ToDateTime(dr["fecha_compra"]),
                                total = Convert.ToDecimal(dr["total"]),
                                id_proveedor = Convert.ToInt32(dr["id_proveedor"]),

                            });
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Compra>(); // Si ocurre un error, devuelve una lista vacía
                }
            }

            return lista;
        }
        public void InsertarCompra(Compra compra)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                // Iniciamos la Traccion 
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("prueba_SP_INSERTAR_COMPRA", con, tran);    
                    cmd.CommandType = CommandType.StoredProcedure;
                   // cmd.Parameters.AddWithValue("@id_compra", compra.id_compra);
                    cmd.Parameters.AddWithValue("@id_usuario", compra.id_usuario);
                    cmd.Parameters.AddWithValue("@id_proveedor", compra.id_proveedor);
                    cmd.Parameters.AddWithValue("@numero_documento", compra.numero_documento);
                    cmd.Parameters.AddWithValue("@total", compra.total);
                    cmd.Parameters.AddWithValue("@fecha_compra", compra.fecha_compra);
                    int CompraId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (Detalle_Compra detalle_Compra in compra.Detalle_Compra)
                    {
                        SqlCommand cmdDetalle = new SqlCommand("InsertarDetalleCompra", con,tran);
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.Parameters.AddWithValue("@iva", detalle_Compra.iva);
                        cmdDetalle.Parameters.AddWithValue("@id_compra", CompraId);
                        cmdDetalle.Parameters.AddWithValue("@id_producto", detalle_Compra.id_producto);
                        cmdDetalle.Parameters.AddWithValue("@cantidad", detalle_Compra.cantidad);
                        cmdDetalle.Parameters.AddWithValue("@precio_unitario", detalle_Compra.precio_unitario);
                        cmdDetalle.Parameters.AddWithValue("@subtotal", detalle_Compra.subtotal);
                        cmdDetalle.Parameters.AddWithValue("@fecha_registro", System.DateTime.Now);
                        cmd.ExecuteNonQuery();

                    }
                    tran.Commit();


                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw new Exception("Error al insertar la compra");
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public Compra SeleccionarCompraConDetalle(int CompraId)
        {
            Compra compra = new Compra();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_SeleccionarCompraConDetalle"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompraId", CompraId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            compra.id_compra = reader.GetInt32(reader.GetOrdinal("CompraId"));
                            compra.fecha_compra = reader.GetDateTime(reader.GetOrdinal("fecha_compra"));
                            compra.total = reader.GetDecimal(reader.GetOrdinal("total"));
                            compra.id_proveedor = reader.GetInt32(reader.GetOrdinal("ProveedorId"));
                        }
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                Detalle_Compra detalle = new Detalle_Compra()
                                {
                                    id_detalle_compra = reader.GetInt32(reader.GetOrdinal("DetalleCompraId")),
                                    iva = reader.GetDecimal(reader.GetOrdinal("iva")),
                                    subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal")),
                                    precio_unitario = reader.GetDecimal(reader.GetOrdinal("precio_unitario")),
                                    cantidad = Convert.ToInt32(reader["cantidad"]),
                                    fecha_registro = reader.GetDateTime(reader.GetOrdinal("fecha_registro")),
                                    id_producto = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                                };

                                compra.Detalle_Compra.Add(detalle);
                            }
                        }
                    }
                }
            }

            return compra;
        }

        public void ActualizarCompra(Compra compra)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // Actualizar datos generales de la compra
                    SqlCommand cmd = new SqlCommand("sp_ActualizarCompra", con, tran);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_compra", compra.id_compra);
                    cmd.Parameters.AddWithValue("@id_usuario", compra.id_usuario);
                    cmd.Parameters.AddWithValue("@id_proveedor", compra.id_proveedor);
                    cmd.Parameters.AddWithValue("@numero_documento", compra.numero_documento);
                    cmd.Parameters.AddWithValue("@total", compra.total);
                    cmd.Parameters.AddWithValue("@fecha_compra", compra.fecha_compra);

                    cmd.ExecuteNonQuery();

                    // Opcional: actualizar detalles
                    // Primero eliminar los detalles anteriores
                    SqlCommand cmdEliminarDetalle = new SqlCommand("sp_EliminarDetalleCompraPorCompraId", con, tran);
                    cmdEliminarDetalle.CommandType = CommandType.StoredProcedure;
                    cmdEliminarDetalle.Parameters.AddWithValue("@id_compra", compra.id_compra);
                    cmdEliminarDetalle.ExecuteNonQuery();

                    // Insertar los nuevos detalles
                    foreach (Detalle_Compra detalle_Compra in compra.Detalle_Compra)
                    {
                        SqlCommand cmdDetalle = new SqlCommand("sp_EliminarDetalleCompraPorCompraId", con, tran);
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.Parameters.AddWithValue("@id_compra", compra.id_compra);
                        cmdDetalle.Parameters.AddWithValue("@id_producto", detalle_Compra.id_producto);
                        cmdDetalle.Parameters.AddWithValue("@cantidad", detalle_Compra.cantidad);
                        cmdDetalle.Parameters.AddWithValue("@precio_unitario", detalle_Compra.precio_unitario);
                        cmdDetalle.Parameters.AddWithValue("@iva", detalle_Compra.iva);
                        cmdDetalle.Parameters.AddWithValue("@subtotal", detalle_Compra.subtotal);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw new Exception("Error al actualizar la compra");
                }
                finally
                {
                    con.Close();
                }
            }

        }
        public void EliminarCompra(int CompraId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // Primero eliminar los detalles de la compra
                    SqlCommand cmdDetalle = new SqlCommand("sp_EliminarDetalleCompraPorCompraId", con, tran);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@id_compra", CompraId);
                    cmdDetalle.ExecuteNonQuery();

                    // Luego eliminar la compra
                    SqlCommand cmdCompra = new SqlCommand("sp_EliminarCompra", con, tran);
                    cmdCompra.CommandType = CommandType.StoredProcedure;
                    cmdCompra.Parameters.AddWithValue("@id_compra", CompraId);
                    cmdCompra.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw new Exception("Error al eliminar la compra");
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}