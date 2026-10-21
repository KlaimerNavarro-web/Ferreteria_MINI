Create Procedure [dbo].[prueba_sp_Insertar_DetalleVenta]
@VentaId int,
@ProductoId int,
@Cantidad int,
@PrecioUnitario decimal (10,2)
as 
begin transaction;
Declare @Stock int;
select @Stock = Stock from Productos where 
IDProducto=@ProductoId;
if @Stock<@Cantidad begin 
rollback transaction;
raisError('Stock insuficiente', 16,1);
Return; end;