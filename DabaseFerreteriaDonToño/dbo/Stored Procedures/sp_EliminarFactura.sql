CREATE PROCEDURE sp_EliminarFactura
    @id_factura INT
AS
BEGIN
    DELETE FROM Factura
    WHERE id_factura = @id_factura;
END;
