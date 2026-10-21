CREATE PROC SP_EliminarPresentacion(
@id_presentacion int,
@Resultado int output,
@mensaje varchar (500) output    
)
as
begin 
     SET @Resultado = 1 
	 IF NOT EXISTS (
	  SELECT * FROM Presentacion p
	  INNER JOIN Producto pr on pr.id_presentacion = p.id_presentacion
	  where p.id_presentacion = @id_presentacion
	 )
	 BEGIN 
	    DELETE TOP(1) FROM Presentacion WHERE id_presentacion = @id_presentacion
	 END

	    	ELSE
		begin
		     SET @Resultado = 0
			 SET @mensaje = 'LA PRESENTACION SE ENCUENTRA RELACIONADA A UN PRODUCTO'

	    end
end
