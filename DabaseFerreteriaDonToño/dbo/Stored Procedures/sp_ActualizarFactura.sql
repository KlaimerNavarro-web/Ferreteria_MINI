CREATE PROCEDURE sp_ActualizarFactura
    @id_factura INT,
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
    UPDATE Factura
    SET
        fecha_venta = @fecha_venta,
        monto_pago = @monto_pago,
        monto_cambio = @monto_cambio,
        numero_comprobante = @numero_comprobante,
        id_cliente = @id_cliente,
        id_usuario = @id_usuario,
        descuento = @descuento,
        total = @total
    WHERE id_factura = @id_factura;
END;
