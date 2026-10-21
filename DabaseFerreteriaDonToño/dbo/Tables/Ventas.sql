CREATE TABLE [dbo].[Ventas] (
    [IdVentas]  INT           IDENTITY (1, 1) NOT NULL,
    [IdUsuario] INT           NULL,
    [fecha]     DATETIME2 (7) NULL,
    [total]     DECIMAL (18)  NULL,
    PRIMARY KEY CLUSTERED ([IdVentas] ASC)
);

