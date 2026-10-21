CREATE TABLE [dbo].[Permiso] (
    [id_permiso]     INT           IDENTITY (1, 1) NOT NULL,
    [id_rol]         INT           NULL,
    [nombre_menu]    NCHAR (100)   NULL,
    [fecha_registro] DATETIME2 (7) DEFAULT (getdate()) NULL,
    CONSTRAINT [FK_Permiso_Rol1] FOREIGN KEY ([id_rol]) REFERENCES [dbo].[Rol] ([id_rol])
);

