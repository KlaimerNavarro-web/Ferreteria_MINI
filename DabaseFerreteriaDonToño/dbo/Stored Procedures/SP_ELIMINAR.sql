CREATE PROCEDURE [dbo].[SP_ELIMINAR]
    @idcategoria INT
AS
BEGIN
    DELETE FROM Categoria
    WHERE id_categoria = @idcategoria
END
