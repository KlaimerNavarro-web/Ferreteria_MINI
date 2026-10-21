CREATE PROCEDURE [dbo].[SP_EDITAR]
    @idcategoria INT,
    @nombre VARCHAR(50)
AS
BEGIN
    UPDATE Categoria
    SET nombre_categoria = @nombre
    WHERE id_categoria = @idcategoria
END
