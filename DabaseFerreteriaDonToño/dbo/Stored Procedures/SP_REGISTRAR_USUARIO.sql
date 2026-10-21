CREATE PROC SP_REGISTRAR_USUARIO(
@nombre varchar (50),
@direccion varchar (100),
@correo varchar(50),
@contraseña varchar (100),
@estado bit,
@id_rol int,
@id_usuarioResultado int output, --parametro de salida 
@mensaje varchar (500) output    --parametro de salida 
)
AS 
BEGIN
   SET @id_usuarioResultado = 0
   SET @mensaje = ''
   
   IF NOT EXISTS(select * from Usuario where correo = @correo)
   BEGIN
      insert into Usuario(nombre,direccion,correo,contraseña,estado,id_rol) values
	  (@nombre,@direccion,@correo,@contraseña,@estado,@id_rol)

	  SET @id_usuarioResultado = SCOPE_IDENTITY()
	  
   END
   ELSE
      SET @mensaje = 'NO SE PUEDE REPETIR EL CORREO PARA MAS DE UN USUARIO'

END;