CREATE PROCEDURE [dbo].[InsertarDetalleCompra]
    @iva DECIMAL(18, 0) = NULL,
    @cantidad INT,
    @precio_unitario DECIMAL(10, 2),
    @id_producto INT,
    @id_compra INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @subtotal DECIMAL(10, 2);

    -- Calcular subtotal automáticamente
    SET @subtotal = @cantidad * @precio_unitario;

    INSERT INTO [dbo].[Detalle_Compra]
    (
        [iva],
        [subtotal],
        [cantidad],
        [precio_unitario],
        [fecha_registro],
        [id_producto],
        [id_compra]
    )
    VALUES
    (
        @iva,
        @subtotal,
        @cantidad,
        @precio_unitario,
        GETDATE(), -- Fecha actual
        @id_producto,
        @id_compra
    );
END
