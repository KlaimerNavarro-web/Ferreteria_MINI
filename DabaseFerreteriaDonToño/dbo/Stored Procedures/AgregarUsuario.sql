CREATE PROCEDURE [dbo].[AgregarUsuario]
    @nombre NVARCHAR(50),
    @direccion NVARCHAR(50),
    @correo NVARCHAR(50),
	 @contraseña NVARCHAR(50),
    @id_roles INT,
    @nuevo_id INT OUTPUT  -- Variable de salida para retornar el ID generado
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Rol WHERE id_rol = @id_roles)
    BEGIN
        INSERT INTO Usuario (nombre, direccion, correo,contraseña, id_rol)
        VALUES (@nombre, @direccion, @correo,@contraseña, @id_roles);

        -- Obtener el ID recién insertado
        SET @nuevo_id = SCOPE_IDENTITY();

        PRINT 'Usuario agregado correctamente.';
    END
    ELSE
    BEGIN
        PRINT 'Error: El rol especificado no existe.';
        SET @nuevo_id = NULL;
    END
END;
