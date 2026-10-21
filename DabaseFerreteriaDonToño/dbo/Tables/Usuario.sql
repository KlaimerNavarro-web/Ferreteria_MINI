CREATE TABLE [dbo].[Usuario] (
    [id_usuario]     INT           IDENTITY (1, 1) NOT NULL,
    [nombre]         NVARCHAR (50) NULL,
    [direccion]      NVARCHAR (50) NULL,
    [correo]         NVARCHAR (50) NULL,
    [contraseña]     NVARCHAR (50) NULL,
    [estado]         BIT           NULL,
    [fecha_registro] DATETIME2 (7) DEFAULT (getdate()) NULL,
    [id_rol]         INT           NULL,
    CONSTRAINT [PK__Usuario__4E3E04ADBF41D84B] PRIMARY KEY CLUSTERED ([id_usuario] ASC),
    CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY ([id_rol]) REFERENCES [dbo].[Rol] ([id_rol])
);

