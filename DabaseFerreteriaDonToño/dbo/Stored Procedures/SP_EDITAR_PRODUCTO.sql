
CREATE PROCEDURE [dbo].[SP_EDITAR_PRODUCTO]
    @id_producto INT,
    @nombre NVARCHAR(100),
    @descripcion NVARCHAR(255),
    @id_presentacion INT,
    @id_categoria INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Producto
    SET
        nombre = @nombre,
        descripcion = @descripcion,
        id_presentacion = @id_presentacion,
        id_categoria = @id_categoria
    WHERE id_producto = @id_producto;
END;
