
CREATE PROC SP_ELIMINAR_PROVEEDOR (
@id_proveedor int,
@Resultado bit output,
@mensaje varchar (500) output
)
AS 
BEGIN 
  SET  @Resultado = 1 
  IF NOT EXISTS (
  SELECT * FROM Proveedor p
  INNER JOIN Compra c on p.id_proveedor = c.id_proveedor
  WHERE P.id_proveedor = @id_proveedor
  )
  BEGIN 
  DELETE TOP(1) FROM Proveedor WHERE id_proveedor = @id_proveedor
  END
  ELSE
  BEGIN
      SET @Resultado = 0
	  SET @mensaje   = 'EL PROVEEDOR SE ENCUENTRA RELACIONADO A UNO COMPRA'

	  END
	  END