CREATE PROC SP_EditarPresentacion(
@id_presentacion int,
@nombre varchar(100),
@unidad_medida varchar (50),
@peso varchar(100),
@estado bit,
@Resultado int output,
@mensaje varchar (500) output    
)
as
begin 
     SET @Resultado = 1 
	 IF NOT EXISTS (SELECT * FROM Presentacion WHERE nombre = @nombre and id_presentacion != @id_presentacion)

	      update Presentacion set
		  nombre = @nombre,
		  unidad_medida = @unidad_medida,
		  peso = @peso,
		  estado = @estado
		  where id_presentacion = @id_presentacion
		ELSE
		begin
		     SET @Resultado = 0
			 SET @mensaje = 'NO SE PUEDE REPETIR EL NOMBRE DE UNA PRESENTACION'

	    end
end
