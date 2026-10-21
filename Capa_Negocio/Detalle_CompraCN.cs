using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class Detalle_CompraCN
    {
        private readonly Detalle_Compra_CD datos;

        public Detalle_CompraCN(string connectionString)
        {
            datos = new Detalle_Compra_CD(connectionString);
        }

        // MÉTODO: INSERTAR
        public bool Insertar(Detalle_Compra detalle)
        {
            // Puedes incluir validaciones antes de enviar a la BD
            if (detalle == null)
                throw new ArgumentNullException("El objeto detalle de compra no puede ser nulo.");

            if (detalle.cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            if (detalle.precio_unitario <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor que cero.");

            if (detalle.id_compra <= 0 || detalle.id_producto <= 0)
                throw new ArgumentException("Debe especificar un ID de compra y un ID de producto válidos.");

            return datos.Insertar(detalle);
        }

        // MÉTODO: LISTAR
 
        public List<Detalle_Compra> Listar()
        {
            return datos.Listar();
        }

        // MÉTODO: ACTUALIZAR

        public bool Actualizar(Detalle_Compra detalle)
        {
            if (detalle == null)
                throw new ArgumentNullException("El objeto detalle de compra no puede ser nulo.");

            if (detalle.id_detalle_compra <= 0)
                throw new ArgumentException("El ID del detalle de compra no es válido.");

            if (detalle.cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            if (detalle.precio_unitario <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor que cero.");

            return datos.Actualizar(detalle);
        }


        // MÉTODO: ELIMINAR

        public bool Eliminar(int id_detalle_compra)
        {
            if (id_detalle_compra <= 0)
                throw new ArgumentException("El ID del detalle de compra no es válido.");

            return datos.Eliminar(id_detalle_compra);
        }


        // MÉTODO: OBTENER POR ID

        public Detalle_Compra ObtenerPorId(int id_detalle_compra)
        {
            if (id_detalle_compra <= 0)
                throw new ArgumentException("El ID del detalle de compra no es válido.");

            return datos.ObtenerPorId(id_detalle_compra);
        }
    }
}

