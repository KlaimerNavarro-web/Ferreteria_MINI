CREATE PROCEDURE [dbo].[SP_ELIMINAR_PRODUCTO]
    @id_producto INT
AS
BEGIN
    UPDATE Producto
    SET estado = 0
    WHERE id_producto = @id_producto;
END;
