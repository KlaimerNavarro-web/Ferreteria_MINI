CREATE TABLE [dbo].[Productos] (
    [IDProducto] INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]     NVARCHAR (50) NULL,
    [Precio]     DECIMAL (18)  NULL,
    [Stock]      INT           NULL,
    CONSTRAINT [PK__Producto__ABDAF2B4B04AF19A] PRIMARY KEY CLUSTERED ([IDProducto] ASC)
);

