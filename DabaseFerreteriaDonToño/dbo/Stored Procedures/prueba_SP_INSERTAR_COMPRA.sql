CREATE PROCEDURE [dbo].[prueba_SP_INSERTAR_COMPRA]
    @id_usuario INT,
    @id_proveedor INT,
    @numero_documento NCHAR(50),
    @total DECIMAL(10,2),
    @fecha_compra DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Compra (id_usuario, id_proveedor, numero_documento, total, fecha_compra)
    VALUES (@id_usuario, @id_proveedor, @numero_documento, @total, @fecha_compra);

    -- Retorna el ID de la compra recién insertada
    SELECT SCOPE_IDENTITY() AS CompraId;
END;
