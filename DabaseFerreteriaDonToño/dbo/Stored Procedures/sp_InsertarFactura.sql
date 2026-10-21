CREATE PROCEDURE sp_InsertarFactura
    @fecha_venta DATETIME,
    @monto_pago DECIMAL(10,2),
    @monto_cambio DECIMAL(10,2),
    @numero_comprobante INT,
    @id_cliente INT,
    @id_usuario INT,
    @descuento DECIMAL(10,2),
    @total DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Factura (fecha_venta, monto_pago, monto_cambio, numero_comprobante, id_cliente, id_usuario, descuento, total)
    VALUES (@fecha_venta, @monto_pago, @monto_cambio, @numero_comprobante, @id_cliente, @id_usuario, @descuento, @total);

    -- Opcional: devolver el id de la factura recién insertada
    SELECT SCOPE_IDENTITY() AS id_factura;
END;
