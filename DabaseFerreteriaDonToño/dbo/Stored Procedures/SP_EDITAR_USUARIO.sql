CREATE PROC SP_EDITAR_USUARIO(
@id_usuario int ,
@nombre varchar (50),
@direccion varchar (100),
@correo varchar(50),
@contraseña varchar (100),
@estado bit,
@id_rol int,
@Repuesta bit  output, --parametro de salida 
@mensaje varchar (500) output    --parametro de salida 
)
AS 
BEGIN
   SET @Repuesta = 0
   SET @mensaje = ''
  -- para guardar los cambios pero valida que el correo no estista otro distinto para el que yo estoy editando 
   IF NOT EXISTS(select * from Usuario where correo = @correo and id_usuario != @id_usuario)
   BEGIN
      UPDATE Usuario SET
	    nombre = @nombre ,
	    direccion =@direccion,
		correo = @correo,
		contraseña = @contraseña,
		estado = @estado,
		id_rol = @id_rol
		where id_usuario = @id_usuario
	  

	  SET @Repuesta = 1
	  
   END
   ELSE
      SET @mensaje = 'NO SE PUEDE REPETIR EL CORREO PARA MAS DE UN USUARIO'

END