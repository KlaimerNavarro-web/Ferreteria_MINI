CREATE PROCEDURE sp_Eliminar_Detalle_Factura
    @id_detalle_factura INT
AS
BEGIN
    DELETE FROM Detalle_Factura WHERE id_detalle_factura = @id_detalle_factura;
END
