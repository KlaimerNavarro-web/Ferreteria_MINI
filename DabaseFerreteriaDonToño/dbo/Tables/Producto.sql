CREATE TABLE [dbo].[Producto] (
    [id_producto]     INT           IDENTITY (1, 1) NOT NULL,
    [nombre]          NVARCHAR (50) NULL,
    [descripcion]     TEXT          NULL,
    [id_categoria]    INT           NOT NULL,
    [id_presentacion] INT           NOT NULL,
    [estado]          BIT           NULL,
    [fecha_registro]  DATETIME2 (7) DEFAULT (getdate()) NULL,
    CONSTRAINT [PK__Producto__3804F4FB583B5BEB] PRIMARY KEY CLUSTERED ([id_producto] ASC),
    CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY ([id_presentacion]) REFERENCES [dbo].[Categoria] ([id_categoria]),
    CONSTRAINT [FK_Producto_Presentacion] FOREIGN KEY ([id_presentacion]) REFERENCES [dbo].[Presentacion] ([id_presentacion])
);

