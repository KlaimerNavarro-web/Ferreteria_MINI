CREATE TABLE [dbo].[Detalle_Compra] (
    [id_detalle_compra] INT             IDENTITY (1, 1) NOT NULL,
    [iva]               DECIMAL (18)    NULL,
    [subtotal]          DECIMAL (10, 2) NULL,
    [cantidad]          INT             NULL,
    [precio_unitario]   DECIMAL (10, 2) NULL,
    [fecha_registro]    DATETIME        NULL,
    [id_producto]       INT             NULL,
    [id_compra]         INT             NOT NULL,
    CONSTRAINT [PK__Detalle___E4ADC1C03D9C50EC] PRIMARY KEY CLUSTERED ([id_detalle_compra] ASC),
    CONSTRAINT [FK_Detalle_Compra_Compra] FOREIGN KEY ([id_compra]) REFERENCES [dbo].[Compra] ([id_compra]),
    CONSTRAINT [FK_Detalle_Compra_Producto] FOREIGN KEY ([id_producto]) REFERENCES [dbo].[Producto] ([id_producto])
);


GO
CREATE TRIGGER trg_ActualizarInventario_Compra
ON dbo.Detalle_Compra
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualiza el stock de los productos que ya existen en Inventario
    UPDATE i
    SET i.stock_actual = ISNULL(i.stock_actual, 0) + 
                         CAST(TRY_CAST(LTRIM(RTRIM(d.cantidad)) AS DECIMAL(10,2)) AS DECIMAL(10,2)),
        i.id_detalle_compra = d.id_detalle_compra,
        i.fecha_emision = GETDATE(),
        i.estado = 1
    FROM Inventario i
    INNER JOIN inserted d ON i.id_producto = d.id_producto;

    -- Inserta los productos nuevos que no existan en Inventario
    INSERT INTO Inventario (id_producto, stock_actual, stock_minimo, id_detalle_compra, fecha_emision, estado)
    SELECT 
        d.id_producto,
        CAST(TRY_CAST(LTRIM(RTRIM(d.cantidad)) AS DECIMAL(10,2)) AS DECIMAL(10,2)) AS stock_actual,
        0 AS stock_minimo,
        d.id_detalle_compra,
        GETDATE() AS fecha_emision,
        1 AS estado
    FROM inserted d
    WHERE d.id_producto NOT IN (SELECT id_producto FROM Inventario);
END;
