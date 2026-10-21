CREATE TABLE [dbo].[Proveedor] (
    [id_proveedor]   INT           IDENTITY (1, 1) NOT NULL,
    [nombre]         NVARCHAR (50) NOT NULL,
    [correo]         NVARCHAR (50) NULL,
    [telefono]       NVARCHAR (20) NULL,
    [fecha_registro] DATETIME2 (7) DEFAULT (getdate()) NULL,
    [documento]      NCHAR (100)   NULL,
    [estado]         BIT           NULL,
    CONSTRAINT [PK__Proveedo__8D3DFE288B703E21] PRIMARY KEY CLUSTERED ([id_proveedor] ASC)
);

