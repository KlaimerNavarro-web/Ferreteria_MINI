CREATE PROC SP_Registrar_Producto (
@nombre varchar (50),
@descripcion varchar (50),
@id_categoria int,
@id_presentacion int,
@estado bit,
@Resultado int out, 
@mensaje varchar (500) output 
)
AS 
BEGIN 
     SET @Resultado = 0 
	 IF NOT EXISTS (SELECT * FROM Producto WHERE nombre = @nombre)
	 BEGIN 
	   INSERT INTO Producto(nombre,descripcion,id_categoria,id_presentacion,estado) values (@nombre,@descripcion,@id_categoria,@id_presentacion,@estado)
	   SET @Resultado = SCOPE_IDENTITY()
	   END 
	   ELSE 
	   SET @mensaje = 'YA EXISTE UN PRODUCTO CON EL MISMO NOMBRE'
	   END
