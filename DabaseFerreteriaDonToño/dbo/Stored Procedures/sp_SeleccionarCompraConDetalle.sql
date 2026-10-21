CREATE PROCEDURE sp_SeleccionarCompraConDetalle
    @CompraId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Primer conjunto de resultados: información general de la compra
    SELECT 
        c.id_compra AS CompraId,
        c.fecha_compra AS Fecha,
        c.total AS Total,
        c.id_proveedor AS ProveedorId
    FROM Compra c
    WHERE c.id_compra = @CompraId;

    -- Segundo conjunto de resultados: detalles de la compra
    SELECT 
        dc.id_detalle_compra AS DetalleCompraId,
        dc.id_compra AS CompraId,
        dc.id_producto AS ProductoId,
        dc.cantidad,
        dc.precio_unitario,
        dc.subtotal,
        dc.iva,
        dc.fecha_registro
    FROM Detalle_Compra dc
    WHERE dc.id_compra = @CompraId;
END;
