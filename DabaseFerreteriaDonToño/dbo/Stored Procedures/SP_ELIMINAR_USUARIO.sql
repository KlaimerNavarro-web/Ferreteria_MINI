
CREATE PROC SP_ELIMINAR_USUARIO(
@id_usuario int ,
@Repuesta bit  output, --parametro de salida 
@mensaje varchar (500) output    --parametro de salida 
)
AS 
BEGIN
   SET @Repuesta = 0
   SET @mensaje = ''
   DECLARE @pasoreglas bit = 1

   --Validar que este usario que deseamo eliminar, no este relacionado a una compra
   -- por que pofriamos tener fututo problema con el historial
   -- si no lo quieres ver en el mantenedor lo puede desactivar 
   -- en caso de que lo elimine es cuendo recien lo creas ejemplo si te confundiste en un dato pero solo lo puedes hacer en el mismo momento 
   -- en el cual no ha sido relacionado en ninguna compra 
   
   IF EXISTS (select * from Compra c --1
   inner join Usuario u on u.id_usuario = c.id_usuario
   where u.id_usuario = @id_usuario
   )
   BEGIN 
      SET @pasoreglas = 0
      SET @Repuesta = 0
      SET @mensaje = @mensaje +'NO SE PUEDE ELIMINAR POR QUE SE ENCUENTRA RELACIONADO A UNA COMPRA\n'
   END

   -- Validar que no este relacionada a una factura 
      IF EXISTS (select * from Factura f --2
   inner join Usuario u on u.id_usuario = f.id_usuario
   where u.id_usuario = @id_usuario
   )
   BEGIN
      SET @pasoreglas = 0
      SET @Repuesta = 0
      SET @mensaje = @mensaje +'NO SE PUEDE ELIMINAR POR QUE SE ENCUENTRA RELACIONADO A UNA FACTURA\n'
   END

   -- siempre y cuendo no esten relacionados se podra eliminar el usuario 
   IF(@pasoreglas = 1)--3
   BEGIN 
       delete from Usuario where id_usuario = @id_usuario
	   SET @Repuesta = 1
   END

END
