using System;
using System.Collections.Generic;
using System.Linq;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class Factura_Negocio
    {
        private readonly string ConnectionString = string.Empty;
        private readonly Factura_CD _facturaDatos;

        // Constructor con cadena de conexión
        public Factura_Negocio(string connectionString)
        {
            ConnectionString = connectionString;
            _facturaDatos = new Factura_CD(ConnectionString);
        }

        // Listar todas las facturas
        public List<Factura> Listar()
        {
            return _facturaDatos.Listar();
        }


        // Método para insertar una factura
        public void InsertarFactura(Factura factura)
        {
            // Validar que haya detalles
            if (factura.Detalle_Factura == null || !factura.Detalle_Factura.Any())
            {
                throw new ArgumentException("La factura debe contener al menos un detalle de producto.");
            }

            // Validar cliente y usuario
            if (factura.id_cliente <= 0)
                throw new ArgumentException("Debe seleccionar un cliente válido.");

            if (factura.id_usuario <= 0)
                throw new ArgumentException("Debe existir un usuario asociado a la factura.");

            // Validar cada detalle
            foreach (var detalle in factura.Detalle_Factura)
            {
                if (detalle.cantidad <= 0)
                    throw new ArgumentException("La cantidad del producto debe ser mayor a cero.");

                if (detalle.precio_venta <= 0)
                    throw new ArgumentException("El precio unitario debe ser mayor a cero.");
            }

            // Asignar fecha actual si no está definida
            if (factura.fecha_venta == default)
                factura.fecha_venta = DateTime.Now;

            // Calcular el total de la factura sumando cada detalle
            factura.total = factura.Detalle_Factura.Sum(d => d.cantidad * d.precio_venta);

            try
            {
                // Llamar a la capa de datos y devolver el Id generado
                _facturaDatos.InsertarFactura(factura);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar la factura: {ex.Message}", ex);
            }
        }

        // Obtener factura con detalle por Id
        public Factura ObtenerFacturaConDetalle(int FacturaId)
        {
            if (FacturaId <= 0)
                throw new ArgumentException("El Id de la factura debe ser válido.");

            return _facturaDatos.SeleccionarPorId(FacturaId);
        }

        // Actualizar factura
        public void ActualizarFactura(Factura factura)
        {
            if (factura.id_factura <= 0)
                throw new ArgumentException("El Id de la factura debe ser válido.");

            // Validar que haya detalles
            if (factura.Detalle_Factura == null || !factura.Detalle_Factura.Any())
                throw new ArgumentException("La factura debe contener al menos un detalle de producto.");

            // Validar cliente y usuario
            if (factura.id_cliente <= 0)
                throw new ArgumentException("Debe seleccionar un cliente válido.");

            if (factura.id_usuario <= 0)
                throw new ArgumentException("Debe existir un usuario asociado a la factura.");

            // Validar cada detalle
            foreach (var detalle in factura.Detalle_Factura)
            {
                if (detalle.cantidad <= 0)
                    throw new ArgumentException("La cantidad del producto debe ser mayor a cero.");

                if (detalle.precio_venta <= 0)
                    throw new ArgumentException("El precio unitario debe ser mayor a cero.");
            }

            // Calcular el total actualizado
            factura.total = factura.Detalle_Factura.Sum(d => d.cantidad * d.precio_venta);

            try
            {
                _facturaDatos.ActualizarFactura(factura);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la factura: {ex.Message}", ex);
            }
        }

        // Eliminar factura
        public void EliminarFactura(int FacturaId)
        {
            if (FacturaId <= 0)
                throw new ArgumentException("El Id de la factura debe ser válido.");

            try
            {
                _facturaDatos.EliminarFactura(FacturaId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la factura: {ex.Message}", ex);
            }
        }
    }
}
