CREATE TABLE [dbo].[Cliente] (
    [id_cliente]     INT           IDENTITY (1, 1) NOT NULL,
    [documento]      NCHAR (50)    NULL,
    [nombre]         NVARCHAR (50) NOT NULL,
    [correo]         NVARCHAR (50) NULL,
    [telefono]       NVARCHAR (20) NULL,
    [fecha_registro] DATETIME2 (7) DEFAULT (getdate()) NULL,
    [estado]         BIT           NULL,
    CONSTRAINT [PK__Cliente__677F38F5A0D0F6BC] PRIMARY KEY CLUSTERED ([id_cliente] ASC)
);

