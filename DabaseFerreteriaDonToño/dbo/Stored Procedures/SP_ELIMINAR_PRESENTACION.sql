CREATE PROCEDURE [dbo].[SP_ELIMINAR_PRESENTACION]
    @id_presentacion INT
AS
BEGIN
    DELETE FROM Presentacion
    WHERE id_presentacion = @id_presentacion;
END;