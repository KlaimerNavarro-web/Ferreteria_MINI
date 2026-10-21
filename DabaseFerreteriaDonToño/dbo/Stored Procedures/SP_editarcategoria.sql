CREATE PROCEDURE SP_editarcategoria(
@id_categoria int,
@nombre_categoria varchar (100),
@descripcion varchar (50),
@estado bit,
@Resultado bit output,
@mensaje varchar (500) output 
)
as
begin 
     SET @Resultado = 1 
	 IF NOT EXISTS (SELECT * FROM Categoria WHERE descripcion = @descripcion and id_categoria !=@id_categoria)

	      update Categoria set
		  descripcion = @descripcion
		  where id_categoria = @id_categoria
		ELSE
		begin
		     SET @Resultado = 0
			 SET @mensaje = 'NO SE PUEDE REPETIR LA DESCRIPCION DE UNA CATEGORIA'

	    end

end

