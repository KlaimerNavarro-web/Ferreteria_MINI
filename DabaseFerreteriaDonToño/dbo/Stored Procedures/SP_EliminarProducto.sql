
CREATE PROC SP_EliminarProducto (
@id_producto int,
@Repuesta int out, 
@mensaje varchar (500) output 
)
AS 
BEGIN 
     SET @Repuesta = 0
	 SET @mensaje = ''
	 DECLARE @Pasoreglas bit = 1

	 IF  EXISTS (SELECT * FROM Detalle_Compra dc
	 INNER JOIN Producto p ON p.id_producto = dc.id_producto
	 WHERE p.id_producto = @id_producto 
	 )
    BEGIN 
	  SET @Pasoreglas = 0
	  SET @Repuesta = 0
	  SET @mensaje = @mensaje + 'NO SE PUEDE ELIMINAR POR QUE SE ENCUENTRA RELACIONADO A UNA COMPRA\n'
	END

	IF EXISTS ( SELECT * FROM Detalle_Factura df
	INNER JOIN Producto p ON p.id_producto = df.id_producto
	WHERE p.id_producto = @id_producto	
	)
	BEGIN 
	  SET @Pasoreglas = 0
	  SET @Repuesta = 0
	  SET @mensaje = @mensaje + 'NO SE PUEDE ELIMINAR POR QUE SE ENCUENTRA RELACIONADO A UNA FACTURA\n'
	END 

	IF (@Pasoreglas = 1)
	BEGIN 
	   DELETE FROM Producto WHERE id_producto = @id_producto
	   SET @Repuesta = 1
	   END

 END
