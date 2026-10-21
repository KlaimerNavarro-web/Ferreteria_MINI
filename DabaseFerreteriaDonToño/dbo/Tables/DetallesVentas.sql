CREATE TABLE [dbo].[DetallesVentas] (
    [IdDetalleVenta] INT          IDENTITY (1, 1) NOT NULL,
    [VentaId]        INT          NULL,
    [ProductoId]     INT          NULL,
    [Cantidad]       INT          NULL,
    [PrecioUnitario] DECIMAL (18) NULL,
    PRIMARY KEY CLUSTERED ([IdDetalleVenta] ASC),
    CONSTRAINT [FK_DetallesVentas_Ventas] FOREIGN KEY ([VentaId]) REFERENCES [dbo].[Ventas] ([IdVentas])
);

