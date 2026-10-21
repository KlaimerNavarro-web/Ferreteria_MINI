CREATE PROCEDURE sp_InicioUsuario
    @correo NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.id_usuario,
        u.nombre,
        u.direccion,
        u.correo,
        u.contraseña,
        u.estado,
        u.fecha_registro,
        u.id_rol,
        r.descripcion AS RolNombre
    FROM Usuario u
    INNER JOIN Rol r ON u.id_rol = r.id_rol
    WHERE u.correo = @correo
      AND u.estado = 1;   -- solo usuarios activos (puedes quitarlo si no lo usas)
END
