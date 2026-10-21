CREATE TABLE [dbo].[Categoria] (
    [id_categoria]     INT            IDENTITY (1, 1) NOT NULL,
    [nombre_categoria] NVARCHAR (50)  NULL,
    [descripcion]      NVARCHAR (256) NULL,
    [estado]           BIT            NULL,
    PRIMARY KEY CLUSTERED ([id_categoria] ASC)
);

