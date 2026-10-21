CREATE TABLE [dbo].[Rol] (
    [id_rol]         INT           IDENTITY (1, 1) NOT NULL,
    [descripcion]    NCHAR (100)   NULL,
    [fecha_registro] DATETIME2 (7) DEFAULT (getdate()) NULL,
    CONSTRAINT [PK__Rol__6ABCB5E02FDDF8A3] PRIMARY KEY CLUSTERED ([id_rol] ASC)
);

