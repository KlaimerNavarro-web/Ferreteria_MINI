CREATE PROCEDURE sp_SeleccionarFacturaPorId
    @id_factura INT
AS
BEGIN
    SELECT *
    FROM Factura
    WHERE id_factura = @id_factura;
END;
