 
CREATE PROC SP_EDITAR_PROVEEDOR(
@id_proveedor int,
@nombre varchar (50),
@correo varchar (50),
@telefono varchar (50),
@documento varchar (50),
@estado bit ,
@Resultado int output,
@mensaje varchar (500) output
)
AS 
BEGIN 
   SET  @Resultado = 1
   DECLARE @id_persona INT
   IF NOT EXISTS (SELECT * FROM Proveedor WHERE documento = @documento and id_proveedor != @id_proveedor)
   BEGIN 
    UPDATE  Proveedor SET
	nombre = @nombre,
	correo = @correo,
	telefono = @telefono,
	documento = @documento,
	estado  =  @estado
	WHERE id_proveedor = @id_proveedor
	END
	ELSE 
	BEGIN
       SET @Resultado = 0
	   SET @mensaje = 'El NUMERO DE DOCUMENTO DEL PREOVEEDOR YA EXTISTE'
	   END
	 END 
