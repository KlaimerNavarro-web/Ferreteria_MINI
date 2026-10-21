CREATE TABLE [dbo].[Inventario] (
    [id_inventario]      INT           IDENTITY (1, 1) NOT NULL,
    [id_producto]        INT           NOT NULL,
    [stock_actual]       INT           CONSTRAINT [DF__Inventari__stock__4DB4832C] DEFAULT ((0)) NULL,
    [stock_minimo]       INT           CONSTRAINT [DF__Inventari__stock__4EA8A765] DEFAULT ((0)) NULL,
    [id_detalle_factura] INT           NULL,
    [fecha_emision]      DATETIME2 (7) NULL,
    [id_detalle_compra]  INT           NULL,
    [estado]             BIT           NULL,
    CONSTRAINT [PK__Inventar__013AEB51B94DA4FC] PRIMARY KEY CLUSTERED ([id_inventario] ASC),
    CONSTRAINT [FK_Inventario_Detalle_Compra] FOREIGN KEY ([id_detalle_compra]) REFERENCES [dbo].[Detalle_Compra] ([id_detalle_compra]),
    CONSTRAINT [FK_Inventario_Detalle_Factura] FOREIGN KEY ([id_detalle_factura]) REFERENCES [dbo].[Detalle_Factura] ([id_detalle_factura])
);

