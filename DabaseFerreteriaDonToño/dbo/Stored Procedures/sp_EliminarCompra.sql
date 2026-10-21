CREATE PROCEDURE sp_EliminarCompra
    @id_compra INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Compra
    WHERE id_compra = @id_compra;
END;
