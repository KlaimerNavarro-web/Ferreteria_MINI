
CREATE PROC SP_REGISTRAR_PROVEEDOR(
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
   SET  @Resultado = 0
   DECLARE @id_persona INT
   IF NOT EXISTS (SELECT * FROM Proveedor WHERE documento = @documento)
   BEGIN 
    INSERT INTO Proveedor(nombre,correo,telefono,documento,estado) values (
	@nombre, @correo,@telefono, @documento, @estado)

    SET @Resultado = SCOPE_IDENTITY()
	END 
	ELSE 
	 SET @mensaje = 'El NUMERO DE DOCUMENTO DEL PREOVEEDOR YA EXTISTE'
	 END 
