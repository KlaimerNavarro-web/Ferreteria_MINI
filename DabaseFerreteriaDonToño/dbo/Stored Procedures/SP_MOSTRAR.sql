CREATE PROCEDURE [dbo].[SP_MOSTRAR]
AS
BEGIN
    SELECT id_categoria, nombre_categoria
    FROM Categoria
    ORDER BY id_categoria DESC
END
