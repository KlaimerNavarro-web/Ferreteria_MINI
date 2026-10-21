using System;
using System.Collections.Generic;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class Detalle_Factura_CN
    {
        private readonly Detalle_Factura_CD datos;

        public Detalle_Factura_CN(string connectionString)
        {
            datos = new Detalle_Factura_CD(connectionString);
        }

        
        // MÉTODO: INSERTAR
        
        public bool Insertar(Detalle_Factura detalle)
        {
            if (detalle == null)
                throw new ArgumentNullException("El detalle de factura no puede ser nulo.");

            if (detalle.cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            if (detalle.precio_venta <= 0)
                throw new ArgumentException("El precio de venta debe ser mayor que cero.");

            return datos.Insertar(detalle);
        }

        
        // MÉTODO: LISTAR
        
        public List<Detalle_Factura> Listar()
        {
            return datos.Listar();
        }

        
        // MÉTODO: ACTUALIZAR
        
        public bool Actualizar(Detalle_Factura detalle)
        {
            if (detalle == null)
                throw new ArgumentNullException("El detalle de factura no puede ser nulo.");

            if (detalle.id_detalle_factura <= 0)
                throw new ArgumentException("Debe especificar un ID de detalle válido.");

            if (detalle.cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            if (detalle.precio_venta <= 0)
                throw new ArgumentException("El precio de venta debe ser mayor que cero.");

            return datos.Actualizar(detalle);
        }

        
        // MÉTODO: ELIMINAR
        
        public bool Eliminar(int id_detalle_factura)
        {
            if (id_detalle_factura <= 0)
                throw new ArgumentException("El ID del detalle de factura no es válido.");

            return datos.Eliminar(id_detalle_factura);
        }

       
        // MÉTODO: OBTENER POR ID
       
        public Detalle_Factura? ObtenerPorId(int id_detalle_factura)
        {
            if (id_detalle_factura <= 0)
                throw new ArgumentException("El ID del detalle de factura no es válido.");

            return datos.ObtenerPorId(id_detalle_factura);
        }
    }
}
