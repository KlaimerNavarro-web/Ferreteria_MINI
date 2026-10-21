CREATE PROCEDURE [dbo].[prueba_SP_INSERTAR_VENTA]
@IdUsuario int,
@fecha datetime,
@total decimal(10,2)
as 
begin 
insert into Ventas(IdUsuario,fecha,total)
values (@IdUsuario,@fecha,@total);

-- Obtenemos el Id de la ventas recien ingresado

select SCOPE_IDENTITY() as VentaId;
end;