CREATE PROCEDURE [dbo].[AgregarRol]
    @nombreRol NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @permiso NVARCHAR(50);

    -- Determinar el permiso según el nombre del rol
    IF @nombreRol = 'Administrador'
        SET @permiso = 'Acceso total';
    ELSE IF @nombreRol = 'Empleado'
        SET @permiso = 'Acceso limitado';
    ELSE
    BEGIN
        RAISERROR('Solo se permiten roles: Administrador o Empleado.', 16, 1);
        RETURN;
    END

    -- Insertar el nuevo rol con su permiso correspondiente
    INSERT INTO Rol (descripcion)
    VALUES (@nombreRol);
END;
