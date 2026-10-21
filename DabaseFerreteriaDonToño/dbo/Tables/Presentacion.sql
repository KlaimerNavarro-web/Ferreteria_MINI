CREATE TABLE [dbo].[Presentacion] (
    [id_presentacion] INT            IDENTITY (1, 1) NOT NULL,
    [nombre]          VARCHAR (100)  NULL,
    [unidad_medida]   VARCHAR (100)  NULL,
    [peso]            NVARCHAR (100) NULL,
    [estado]          BIT            CONSTRAINT [DF__Presentac__estad__03F0984C] DEFAULT ((1)) NULL,
    CONSTRAINT [PK__Presenta__5F94E0EDB425FFB7] PRIMARY KEY CLUSTERED ([id_presentacion] ASC)
);

