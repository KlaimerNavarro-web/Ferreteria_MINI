CREATE PROCEDURE sp_EliminarDetalleCompraPorCompraId
    @id_compra INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Detalle_Compra
    WHERE id_compra = @id_compra;
END;
