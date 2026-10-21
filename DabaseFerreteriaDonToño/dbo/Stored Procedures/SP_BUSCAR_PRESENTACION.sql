CREATE PROCEDURE [dbo].[SP_BUSCAR_PRESENTACION] 
    @textobuscar VARCHAR(50)
AS
BEGIN
    SELECT id_presentacion, unidad_medida, peso, estado
    FROM Presentacion
    WHERE unidad_medida LIKE '%' + @textobuscar + '%'
      AND estado = 1
END
