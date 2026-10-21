using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class Compra_Negocio
    {
        private readonly string ConnectionString = string.Empty;
        private readonly Compra_CD _compraDatos;

        public List<Compra> Listar()
        {
            return _compraDatos.Listar(); // retornando la lista que se encuentra en la capa datos
        }

        // Constructor con cadena de conexión
        public Compra_Negocio(string connectionString)
        {
            ConnectionString = connectionString;
            _compraDatos = new Compra_CD(ConnectionString);
        }

        // Método para insertar una compra
        public void InsertarCompra(Compra compra)
        {
            // Validar que haya detalles
            if (compra.Detalle_Compra == null || !compra.Detalle_Compra.Any())
            {
                throw new ArgumentException("La compra debe contener al menos un detalle de producto.");
            }

            // Validar proveedor y usuario
            if (compra.id_proveedor <= 0)
                throw new ArgumentException("Debe seleccionar un proveedor válido.");

            if (compra.id_usuario <= 0)
                throw new ArgumentException("Debe existir un usuario asociado a la compra.");

            // Validar cada detalle
            foreach (var detalle in compra.Detalle_Compra)
            {
                if (detalle.cantidad <= 0)
                {
                    throw new ArgumentException("La cantidad del producto debe ser mayor a cero.");
                }

                if (detalle.precio_unitario <= 0)
                {
                    throw new ArgumentException("El precio unitario debe ser mayor a cero.");
                }
            }

            // Asignar fecha actual si no está definida
            if (compra.fecha_compra == default)
                compra.fecha_compra = DateTime.Now;

            // Calcular el total de la compra
            compra.total = compra.Detalle_Compra.Sum(d => d.cantidad * d.precio_unitario);

            try
            {
                // Llamar a la capa de datos
                _compraDatos.InsertarCompra(compra);
            }
            catch (Exception ex)
            {
                // En caso de error, encapsular mensaje
                throw new Exception($"Error al registrar la compra: {ex.Message}", ex);
            }
        }

        public Compra ObtenerCompraConDetalle(int CompraId)
        {
            if (CompraId <= 0)
                throw new ArgumentException("El Id de la compra debe estar lleno");
            return _compraDatos.SeleccionarCompraConDetalle(CompraId);
        }
    
        public void ActualizarCompra(Compra compra)
        {
            if (compra.id_compra <= 0)
                throw new ArgumentException("El Id de la compra debe ser válido.");

            // Validar que haya detalles
            if (compra.Detalle_Compra == null || !compra.Detalle_Compra.Any())
            {
                throw new ArgumentException("La compra debe contener al menos un detalle de producto.");
            }

            // Validar proveedor y usuario
            if (compra.id_proveedor <= 0)
                throw new ArgumentException("Debe seleccionar un proveedor válido.");

            if (compra.id_usuario <= 0)
                throw new ArgumentException("Debe existir un usuario asociado a la compra.");

            // Validar cada detalle
            foreach (var detalle in compra.Detalle_Compra)
            {
                if (detalle.cantidad <= 0)
                {
                    throw new ArgumentException("La cantidad del producto debe ser mayor a cero.");
                }

                if (detalle.precio_unitario <= 0)
                {
                    throw new ArgumentException("El precio unitario debe ser mayor a cero.");
                }
            }

            // Calcular el total actualizado
            compra.total = compra.Detalle_Compra.Sum(d => d.cantidad * d.precio_unitario);

            try
            {
                _compraDatos.ActualizarCompra(compra);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la compra: {ex.Message}", ex);
            }
        }

        public void EliminarCompra(int CompraId)
        {
            if (CompraId <= 0)
                throw new ArgumentException("El Id de la compra debe ser válido.");

            try
            {
                _compraDatos.EliminarCompra(CompraId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la compra: {ex.Message}", ex);
            }
        }
    }
}