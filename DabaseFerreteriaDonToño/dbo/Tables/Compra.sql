CREATE TABLE [dbo].[Compra] (
    [id_compra]        INT             IDENTITY (1, 1) NOT NULL,
    [id_usuario]       INT             NULL,
    [id_proveedor]     INT             NULL,
    [numero_documento] NCHAR (50)      NULL,
    [total]            DECIMAL (10, 2) NULL,
    [fecha_compra]     DATETIME        CONSTRAINT [DF__Compra__fecha_co__178D7CA5] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK__Compras__5909349048621A18] PRIMARY KEY CLUSTERED ([id_compra] ASC),
    CONSTRAINT [FK__Compras__id_prov__11D4A34F] FOREIGN KEY ([id_proveedor]) REFERENCES [dbo].[Proveedor] ([id_proveedor]),
    CONSTRAINT [FK_Compra_Usuario] FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[Usuario] ([id_usuario])
);

