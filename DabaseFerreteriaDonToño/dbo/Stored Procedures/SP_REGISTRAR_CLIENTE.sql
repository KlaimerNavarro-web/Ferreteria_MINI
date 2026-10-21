CREATE PROC SP_REGISTRAR_CLIENTE(
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
   SET  @Resultado = 0
   DECLARE @id_persona INT
   IF NOT EXISTS (SELECT * FROM Cliente WHERE documento = @documento)
   BEGIN 
    INSERT INTO Cliente(documento,nombre,correo,telefono,estado) values (
	 @documento,@nombre,@correo,@telefono,@estado)

    SET @Resultado = SCOPE_IDENTITY()
	END 
	ELSE 
	 SET @mensaje = 'El NUMERO DE DOCUMENTO YA EXTISTE'
	 END 
