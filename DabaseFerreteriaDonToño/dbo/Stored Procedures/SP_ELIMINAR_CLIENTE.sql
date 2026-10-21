
CREATE PROC SP_ELIMINAR_CLIENTE(
    @id_cliente INT,
    @Resultado BIT OUTPUT,
    @mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1

    -- Verifica si el cliente está relacionado con alguna venta
    IF NOT EXISTS (
        SELECT *
        FROM Cliente c
        INNER JOIN Factura f ON f.id_cliente = c.id_cliente
        WHERE c.id_cliente = @id_cliente
    )
    BEGIN
        DELETE TOP(1) FROM Cliente WHERE id_cliente = @id_cliente
    END
    ELSE
    BEGIN
        SET @Resultado = 0
        SET @mensaje = 'EL CLIENTE SE ENCUENTRA RELACIONADO A UNA FACTURA'
    END
END