CREATE PROCEDURE [dbo].[spbuscar_categoria]
    @textobuscar VARCHAR(50)
AS
BEGIN
    SELECT id_categoria, nombre_categoria, descripcion, estado
    FROM Categoria
    WHERE nombre_categoria LIKE '%' + @textobuscar + '%'
    and estado = 1
END
