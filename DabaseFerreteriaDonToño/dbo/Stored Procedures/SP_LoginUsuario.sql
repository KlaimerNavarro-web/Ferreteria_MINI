CREATE PROCEDURE [dbo].[SP_LoginUsuario]
    @correo NVARCHAR(50),
    @contraseña NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        U.id_usuario,
        U.nombre,
        U.direccion,
        U.correo,
        U.id_rol,
        R.id_rol AS rol
    FROM 
        Usuario U
    INNER JOIN 
        Rol R ON U.id_rol = R.id_rol
    WHERE 
        U.correo = @correo AND U.contraseña = @contraseña;
END;
