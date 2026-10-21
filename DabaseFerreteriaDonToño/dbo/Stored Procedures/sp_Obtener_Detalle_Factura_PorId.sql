CREATE PROCEDURE sp_Obtener_Detalle_Factura_PorId
    @id_detalle_factura INT
AS
BEGIN
    SELECT * FROM Detalle_Factura WHERE id_detalle_factura = @id_detalle_factura;
END
