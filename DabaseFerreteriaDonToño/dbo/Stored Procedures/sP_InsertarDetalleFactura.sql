CREATE PROCEDURE [dbo].[sP_InsertarDetalleFactura]
    @id_producto INT,
    @precio_venta NVARCHAR(10), -- o NCHAR(10), lo recibiremos como texto
    @cantidad INT,
    @iva DECIMAL(10, 2) = NULL,
    @id_factura INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @precio_venta_decimal DECIMAL(10, 2);
    DECLARE @subtotal DECIMAL(10, 2);

    -- Convertir el precio_venta a decimal
    SET @precio_venta_decimal = CAST(@precio_venta AS DECIMAL(10, 2));

    -- Calcular subtotal automáticamente
    SET @subtotal = @cantidad * @precio_venta_decimal;

    INSERT INTO [dbo].[Detalle_Factura]
    (
        [id_producto],
        [precio_venta],
        [cantidad],
        [subtotal],
        [iva],
        [fecha_registro],
        [id_factura]
    )
    VALUES
    (
        @id_producto,
        @precio_venta,
        @cantidad,
        @subtotal,
        @iva,
        GETDATE(), -- Fecha actual
        @id_factura
    );
END
