CREATE TABLE [dbo].[Factura] (
    [id_factura]         INT             IDENTITY (1, 1) NOT NULL,
    [fecha_venta]        DATETIME        CONSTRAINT [DF__Facturas__fecha___473C8FC7] DEFAULT (getdate()) NULL,
    [monto_pago]         DECIMAL (10, 2) NULL,
    [monto_cambio]       DECIMAL (10, 2) NULL,
    [numero_comprobante] NCHAR (100)     NULL,
    [id_cliente]         INT             NULL,
    [id_usuario]         INT             NULL,
    [descuento]          DECIMAL (10, 2) NULL,
    [total]              DECIMAL (10, 2) NULL,
    CONSTRAINT [PK__Facturas__7A53152B6ACFF3E7] PRIMARY KEY CLUSTERED ([id_factura] ASC),
    CONSTRAINT [FK__Facturas__id_cli__4830B400] FOREIGN KEY ([id_cliente]) REFERENCES [dbo].[Cliente] ([id_cliente]),
    CONSTRAINT [FK_Factura_Usuario] FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario] ([id_usuario])
);

