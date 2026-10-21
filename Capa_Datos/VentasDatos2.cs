using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Microsoft.Data.SqlClient;
using System.Data;
namespace Capa_Datos
{
    public class VentasDatos2
    {
        public interface IVentaDatos
        {
            Venta2 SeleccionarVentaConDetalles(int VentaID);
            int InsertarVentaConDetalles(Venta2 venta);
        }

        private readonly string ConnectionString;
        public VentasDatos2(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Insertarventa(Venta2 venta)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                //Iniciamos una transaccion 
                SqlTransaction tran = con.BeginTransaction();
                try
                {

                    SqlCommand cmd = new SqlCommand("prueba_SP_INSERTAR_VENTA", con, tran);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                    cmd.Parameters.AddWithValue("@fecha", venta.fecha);
                    cmd.Parameters.AddWithValue("@total", venta.total);
                    int VentaId = Convert.ToInt32(cmd.ExecuteScalar());
                    //int VentaId = (int) cmd.ExecuteScalar();
                    //Insertamos los detalles de la venta
                    foreach (DetallesVentas2 detalle in venta.DetallesVentas)
                    {
                        SqlCommand cmdDetalle = new SqlCommand("prueba_sp_Insertar_DetalleVenta", con);
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.Parameters.AddWithValue("@VentaId", VentaId);
                        cmdDetalle.Parameters.AddWithValue("@ProductoId", detalle);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle);
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle);
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();

                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw new Exception("Error al insertar la venta");
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public Venta2 SeleccionarVentaConDetalle(int VentaId)
        {
            Venta2 venta = new Venta2();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_SeleccionarDetallesVentaPorVentaId"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VentaId", VentaId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            venta.IdVentas = reader.GetInt32(reader.GetOrdinal("VentaId"));

                            //completar los demas campos fecha y total 


                        }

                        /*if (reader.NextResult())
                        {
                            while (reader.Read())
                                DetallesVentas2 detalles = new DetallesVentas2();
                            {
                                Id_DetalleVenta = reader.GetInt32(reader.GetOrdinal("DetalleVentaId"));
                                //Agregar los demas campos
                                venta.DetallesVentas.Add(detalle);
                            }
                        }*/
                    }
                    return venta;
                }
            }
        }
    }
}