
CREATE PROCEDURE [dbo].[SP_EDITAR_DETALLE_COMPRA]
    @id_Detalles_Compras INT,
    @nombre VARCHAR(100),
    @precio_compra DECIMAL(18,2),
    @precio_unitario DECIMAL(18,2),
    @cantidad INT,
    @fecha_compra DATE,
    @Stock_inicial DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Detalle_Compra
    SET
        precio_unitario = @precio_unitario,
        cantidad = @cantidad
    WHERE id_detalle_compra = @id_Detalles_Compras;
END;
