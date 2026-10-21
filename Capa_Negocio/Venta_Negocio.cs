using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;
using static Capa_Datos.VentasDatos2;


namespace Capa_Negocio
{
    public class Venta_Negocio
    {
        private readonly IVentaDatos _Ventadatos_mock;
        public Venta_Negocio(IVentaDatos _Ventadatos)
        {
            _Ventadatos_mock = _Ventadatos;
        }

        private readonly string ConnectionString = string.Empty;
        private VentasDatos2 _ventaDatos;
        public Venta_Negocio(string connectionString)
        {
            ConnectionString = connectionString;
            _ventaDatos=new VentasDatos2(ConnectionString);
        }

        public void InsertarVenta(Venta2 venta)
        {
            if (venta.DetallesVentas == null || !venta.DetallesVentas.Any())
            {
                throw new ArgumentException("La venta debe de tener al menos un detalle");
            }

            //if (venta.TipoPago=="Tarjeta" && venta.pago<100)

            foreach (var detalle in venta.DetallesVentas)
            {
                if (detalle.Cantidad <= 0)
                {
                    throw new ArgumentException("La cantidad debe ser mayor a cero");
                }
                //Validacion del precio

                if (detalle.PrecioUnitario <= 0)
                {
                    throw new ArgumentException("El precio unitario debe ser mayor a cero");
                }
                venta.fecha=DateTime.Now;
                venta.total=venta.DetallesVentas.Sum(d=>d.Cantidad * d.PrecioUnitario);
                _ventaDatos.Insertarventa(venta);
            }

        }
       
        public Venta2 ObtenerVentaConDetalle(int VentaId)
        {
            if (VentaId <= 0)
                throw new ArgumentException("El Id de la venta debe estar lleno");
            return _ventaDatos.SeleccionarVentaConDetalle(VentaId);
        }
    }
}
