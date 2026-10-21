CREATE PROC SP_EDITAR_CLIENTE(
@id_cliente int, 
@documento varchar (50),
@nombre varchar (50),
@correo varchar (50),
@telefono varchar (50),
@estado bit ,
@Resultado int output,
@mensaje varchar (500) output
)
AS 
BEGIN 
   SET  @Resultado = 1 
   DECLARE @id_persona INT
   IF NOT EXISTS (SELECT * FROM Cliente WHERE documento = @documento and id_cliente != @id_cliente)
   BEGIN 
    UPDATE Cliente SET 
	documento = @documento,
	nombre = @nombre,
	correo  = @correo,
	telefono = @telefono,
	estado = @estado
	WHERE id_cliente = @id_cliente
	END 
	ELSE 
	BEGIN  
        SET @Resultado = 0
	    SET @mensaje = 'El NUMERO DE DOCUMENTO YA EXTISTE'
	 END 
	 END 
