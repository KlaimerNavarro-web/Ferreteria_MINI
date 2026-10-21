--Procedimiento para guardar Categoria
CREATE PROC SP_REGISTRAR_CATEGORIA(
@nombre_categoria varchar(100),
@descripcion varchar (50),
@estado bit,
@Resultado int output,
@mensaje varchar (500) output    
)as
begin 
    SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Categoria WHERE descripcion = @descripcion)
	begin
	     insert into Categoria(nombre_categoria, descripcion,estado) values (@nombre_categoria, @descripcion, @estado)
		 set @Resultado = SCOPE_IDENTITY()
      end
	  ELSE
		SET @mensaje = 'NO SE PUEDE REPETIR LA DESCRIPCION DE UNA CATEGORIA'
end 

