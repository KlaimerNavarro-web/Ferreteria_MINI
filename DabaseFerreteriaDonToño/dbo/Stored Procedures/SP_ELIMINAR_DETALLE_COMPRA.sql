CREATE   PROCEDURE [SP_ELIMINAR_DETALLE_COMPRA]
    @id_detalle_compra INT
AS
BEGIN
    DELETE FROM Detalle_Compra WHERE id_detalle_compra = @id_detalle_compra;
END;
