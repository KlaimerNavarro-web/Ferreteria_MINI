Create PROCEDURE [dbo].[sp_SeleccionarDetallesVentaPorVentaId]
@VentaId INT
AS
BEGIN
 SET NOCOUNT ON;

 SELECT 
 DV.IdDetalleVenta,
 DV.VentaId,
 Dv.ProductoId,
 P.Nombre AS NombreProducto,
 DV.Cantidad,
 DV.PrecioUnitario,
 (DV.Cantidad * DV.PrecioUnitario) AS total
 FROM DetallesVentas DV
 INNER JOIN Productos P ON DV.ProductoId = P.IDProducto
 WHERE DV.VentaId = @VentaId;
 END;