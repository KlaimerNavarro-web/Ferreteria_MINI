CREATE PROCEDURE SP_eliminarcategoria(
@id_categoria int,
@Resultado bit output,
@mensaje varchar (500) output 
)
as
begin 
     SET @Resultado = 1 
	 IF NOT EXISTS (
	 select * from Categoria c
	 inner join Producto p on p.id_categoria = c.id_categoria
	 where c.id_categoria = @id_categoria
	 )
	 begin
	   delete top(1) from Categoria where id_categoria = @id_categoria
	 end

	ELSE
	begin
		     SET @Resultado = 0
			 SET @mensaje = 'LA CATEGORIA SE ENCUENTRA RELACIONADA A UN PRODUCTO'

	    end

end

