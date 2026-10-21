CREATE TABLE [dbo].[Detalle_Factura] (
    [id_detalle_factura] INT             IDENTITY (1, 1) NOT NULL,
    [id_producto]        INT             NULL,
    [precio_venta]       NCHAR (10)      NULL,
    [cantidad]           INT             NULL,
    [subtotal]           DECIMAL (10, 2) NULL,
    [iva]                DECIMAL (10, 2) NULL,
    [fecha_registro]     DATETIME2 (7)   NULL,
    [id_factura]         INT             NOT NULL,
    CONSTRAINT [PK__Detalle___5870CF2483978423] PRIMARY KEY CLUSTERED ([id_detalle_factura] ASC),
    CONSTRAINT [FK_Detalle_Factura_Factura] FOREIGN KEY ([id_factura]) REFERENCES [dbo].[Factura] ([id_factura]),
    CONSTRAINT [FK_Detalle_Factura_Producto] FOREIGN KEY ([id_producto]) REFERENCES [dbo].[Producto] ([id_producto])
);


GO
CREATE TRIGGER tr_disminuir_stock_venta
ON Detalle_Factura
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    ------------------------------------------------------
    -- 1. DISMINUIR EL STOCK EN INVENTARIO
    ------------------------------------------------------
    UPDATE inv
    SET inv.stock_actual = inv.stock_actual - i.cantidad
    FROM Inventario inv
    INNER JOIN inserted i ON inv.id_producto = i.id_producto;

    ------------------------------------------------------
    -- 2. VALIDAR STOCK <= 5 Y MOSTRAR ALERTA
    ------------------------------------------------------
    DECLARE @producto NVARCHAR,
            @stock_actual INT;

    SELECT 
        @producto = p.nombre,
        @stock_actual = inv.stock_actual
    FROM Inventario inv
    INNER JOIN inserted i ON inv.id_producto = i.id_producto
    INNER JOIN Producto p ON p.id_producto = i.id_producto;

    IF (@stock_actual <= 5)
    BEGIN
        RAISERROR('⚠️ ¡ALERTA! El producto %s tiene bajo stock. Stock actual: %d (menor o igual a 5)', 
                  16, 1, @producto, @stock_actual);
    END
END;
