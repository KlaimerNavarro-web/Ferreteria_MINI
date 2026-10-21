CREATE PROCEDURE sp_Insertar_CompraConDetalles
    @id_usuario INT,
    @id_proveedor INT,
    @numero_documento NCHAR(50),
    @total DECIMAL(10,2),
    @fecha_compra DATETIME
    --@Detalles TipoDetalleCompra READONLY
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @id_compra INT;

        -- 1️⃣ Insertar la compra
        INSERT INTO Compra (id_usuario, id_proveedor, numero_documento, total, fecha_compra)
        VALUES (@id_usuario, @id_proveedor, @numero_documento, @total, @fecha_compra);

        SET @id_compra = SCOPE_IDENTITY();

        -- 2️⃣ Insertar todos los detalles de la tabla recibida
        INSERT INTO Detalle_Compra (iva, subtotal, cantidad, precio_unitario, fecha_registro, id_producto, id_compra)
        SELECT 
            d.iva, 
            d.subtotal, 
            CAST(d.cantidad AS NCHAR(100)), 
            d.precio_unitario, 
            GETDATE(), 
            d.id_producto, 
            @id_compra
        FROM Detalle_Compra AS d;

        COMMIT TRANSACTION;
        PRINT 'Compra y detalles insertados correctamente.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Error al insertar la compra y sus detalles.';
        THROW;
    END CATCH
END
