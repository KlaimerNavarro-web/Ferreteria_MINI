CREATE PROCEDURE [dbo].[SP_EDITAR_PRESENTACION]
    @id_presentacion INT,
    @unidad_medida NVARCHAR(50),
	@nombre NVARCHAR (100),
    @peso DECIMAL(10, 3)
   -- @estado BIT
AS
BEGIN
    UPDATE Presentacion
    SET unidad_medida = @unidad_medida,
	    nombre = @nombre,
         peso = @peso
       -- estado = @estado
    WHERE id_presentacion = @id_presentacion;
END;