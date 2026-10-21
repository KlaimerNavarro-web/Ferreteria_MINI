CREATE PROCEDURE [dbo].[SP_ELIMINAR_CATEGORIA]
    @idcategoria INT
AS
BEGIN
    UPDATE Categoria
	SET estado = 0
    WHERE id_categoria = @idcategoria
END
