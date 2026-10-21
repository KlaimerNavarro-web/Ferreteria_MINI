CREATE PROCEDURE sp_Insertar_Detalle_Factura
    @id_producto INT,
    @id_factura INT,
    @precio_venta INT,
    @cantidad DECIMAL(10,2),
    @iva DECIMAL(10,2),
    @fecha_registro DATETIME
AS
BEGIN
    INSERT INTO Detalle_Factura (id_producto, id_factura, precio_venta, cantidad, iva, fecha_registro)
    VALUES (@id_producto, @id_factura, @precio_venta, @cantidad, @iva, @fecha_registro);
END
