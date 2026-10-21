
CREATE PROC SP_EditarProducto (
@id_producto int,
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
     SET @Resultado = 1
	 IF NOT EXISTS (SELECT * FROM Producto WHERE nombre = @nombre and id_producto != @id_producto)

	   UPDATE Producto SET 
	   nombre = @nombre,
	   descripcion = @descripcion,
	   id_categoria = @id_categoria,
	   id_presentacion = @id_presentacion,
	   estado = @estado
	   WHERE id_producto = @id_producto
	ELSE 
   BEGIN
	   SET @Resultado = 0
	   SET @mensaje = 'YA EXISTE UN PRODUCTO CON EL MISMO NOMBRE'
 END
 END
