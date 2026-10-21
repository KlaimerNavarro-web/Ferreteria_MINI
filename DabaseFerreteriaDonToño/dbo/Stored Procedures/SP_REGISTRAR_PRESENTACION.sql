CREATE PROC SP_REGISTRAR_PRESENTACION(
@nombre varchar(100),
@unidad_medida varchar (50),
@peso varchar(100),
@estado bit,
@Resultado int output,
@mensaje varchar (500) output    
)
as
begin 
    SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Presentacion WHERE nombre = @nombre)
	begin
	     insert into Presentacion(nombre,unidad_medida, peso, estado) values (@nombre,@unidad_medida, @peso, @estado)
		 set @Resultado = SCOPE_IDENTITY()
      end
	  ELSE
		SET @mensaje = 'NO SE PUEDE REPETIR EL NOMBRE DE UNA PRESENTACION'
end 

