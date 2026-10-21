CREATE PROCEDURE sp_ActualizarCompra
    @id_compra INT,
    @id_usuario INT,
    @id_proveedor INT,
    @numero_documento NVARCHAR(50),
    @total DECIMAL(10,2),
    @fecha_compra DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Compra
    SET 
        id_usuario = @id_usuario,
        id_proveedor = @id_proveedor,
        numero_documento = @numero_documento,
        total = @total,
        fecha_compra = @fecha_compra
    WHERE id_compra = @id_compra;
END;
