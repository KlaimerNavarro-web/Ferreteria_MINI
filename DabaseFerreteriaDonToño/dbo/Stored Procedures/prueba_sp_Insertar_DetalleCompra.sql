CREATE PROCEDURE [dbo].[prueba_sp_Insertar_DetalleCompra]
    @id_compra INT,
    @id_producto INT,
	@iva DECIMAL(18,0),
    @cantidad INT,
    @precio_unitario DECIMAL(10,2),
    @subtotal DECIMAL(10,2),
	@fecha_registro datetime

AS
BEGIN
   -- BEGIN TRY
      --  BEGIN TRANSACTION;

        ---- Verificar que el producto exista
        --IF NOT EXISTS (SELECT 1 FROM Producto WHERE id_producto = @id_producto)
        --BEGIN
        --    RAISERROR('El producto no existe.', 16, 1);
        --    ROLLBACK TRANSACTION;
        --    RETURN;
        --END;

        -- Insertar el detalle de compra
        INSERT INTO Detalle_Compra(id_compra, id_producto, cantidad, precio_unitario, subtotal,iva,fecha_registro)
        VALUES (@id_compra, @id_producto, @cantidad, @precio_unitario, @cantidad * @precio_unitario,@iva,@fecha_registro);

    --    -- Verificar si el producto ya está en inventario
    --    IF EXISTS (SELECT 1 FROM Inventario WHERE id_producto = @id_producto)
    --    BEGIN
    --        -- Actualizar el stock existente
    --        UPDATE Inventario
    --        SET stock_actual = stock_actual + @cantidad
    --        WHERE id_producto = @id_producto;
    --    END
    --    ELSE
    --    BEGIN
    --        -- Insertar nuevo registro en inventario
    --        INSERT INTO Inventario (id_producto, stock_actual)
    --        VALUES (@id_producto, @cantidad);
    --    END

    --    COMMIT TRANSACTION;
    --END TRY
    --BEGIN CATCH
    --    ROLLBACK TRANSACTION;

    --    DECLARE @ErrorMessage NVARCHAR(4000);
    --    DECLARE @ErrorSeverity INT;
    --    DECLARE @ErrorState INT;

    --    SELECT 
    --        @ErrorMessage = ERROR_MESSAGE(),
    --        @ErrorSeverity = ERROR_SEVERITY(),
    --        @ErrorState = ERROR_STATE();

    --    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    --END CATCH
END