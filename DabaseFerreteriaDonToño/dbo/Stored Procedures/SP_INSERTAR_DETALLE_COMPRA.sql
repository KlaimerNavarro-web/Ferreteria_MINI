CREATE   PROCEDURE [SP_INSERTAR_DETALLE_COMPRA]
    @iva DECIMAL(10,2),
    @subtotal DECIMAL(10,2),
    @precio_unitario DECIMAL(10,2),
    @cantidad INT,
    @fecha_registro DATETIME,
    @id_compra INT,
    @id_producto INT
AS
BEGIN
    INSERT INTO Detalle_Compra (iva, subtotal, precio_unitario, cantidad, fecha_registro, id_compra, id_producto)
    VALUES (@iva, @subtotal, @precio_unitario, @cantidad, @fecha_registro, @id_compra, @id_producto);
END;
