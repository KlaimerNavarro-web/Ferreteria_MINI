
CREATE PROCEDURE [dbo].[SP_INSERTAR_PRODUCTO]
    @nombre NVARCHAR(100),
    @descripcion NVARCHAR(255),
    @precio DECIMAL(18,2),
    @stock_final INT,
    @id_presentacion INT,
    @id_categoria INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.Producto
            (nombre, descripcion, id_presentacion, id_categoria, estado)
        VALUES
            (@nombre, @descripcion, @id_presentacion, @id_categoria, 1);

        DECLARE @id_producto INT = SCOPE_IDENTITY();

        INSERT INTO dbo.Inventario
            (id_producto, stock_actual, stock_minimo, estado)
        VALUES
            (@id_producto, @stock_final, 0, 1);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
