CREATE PROCEDURE sp_Actualizar_Detalle_Factura
    @id_detalle_factura INT,
    @id_producto INT,
    @id_factura INT,
    @precio_venta INT,
    @cantidad DECIMAL(10,2),
    @iva DECIMAL(10,2),
    @fecha_registro DATETIME
AS
BEGIN
    UPDATE Detalle_Factura
    SET id_producto = @id_producto,
        id_factura = @id_factura,
        precio_venta = @precio_venta,
        cantidad = @cantidad,
        iva = @iva,
        fecha_registro = @fecha_registro
    WHERE id_detalle_factura = @id_detalle_factura;
END
