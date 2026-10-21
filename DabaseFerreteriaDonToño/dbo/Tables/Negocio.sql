CREATE TABLE [dbo].[Negocio] (
    [id_negocio] INT             NOT NULL,
    [Nombre]     VARCHAR (60)    NULL,
    [RUC]        VARCHAR (60)    NULL,
    [Direccion]  VARCHAR (60)    NULL,
    [Logo]       VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id_negocio] ASC)
);

