CREATE   PROCEDURE sp_Obtener_Detalle_Compra_PorId
    @id_detalle_compra INT
AS
BEGIN
    SELECT * FROM Detalle_Compra WHERE id_detalle_compra = @id_detalle_compra;
END;
