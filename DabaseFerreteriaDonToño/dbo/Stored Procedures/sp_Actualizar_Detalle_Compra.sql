CREATE   PROCEDURE sp_Actualizar_Detalle_Compra
    @id_detalle_compra INT,
    @iva DECIMAL(10,2),
    @subtotal DECIMAL(10,2),
    @precio_unitario DECIMAL(10,2),
    @cantidad INT,
    @fecha_registro DATETIME,
    @id_compra INT,
    @id_producto INT
AS
BEGIN
    UPDATE Detalle_Compra
    SET iva = @iva,
        subtotal = @subtotal,
        precio_unitario = @precio_unitario,
        cantidad = @cantidad,
        fecha_registro = @fecha_registro,
        id_compra = @id_compra,
        id_producto = @id_producto
    WHERE id_detalle_compra = @id_detalle_compra;
END;
