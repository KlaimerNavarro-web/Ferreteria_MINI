CREATE PROCEDURE [dbo].[SP_EDITAR_CATEGORIA]
    @id_categoria INT,
    @nombre_categoria VARCHAR(50),
	@descripcion VARCHAR(100)
AS
BEGIN
    UPDATE Categoria
    SET nombre_categoria = @nombre_categoria,
	descripcion = @descripcion
    WHERE id_categoria = @id_categoria
END
