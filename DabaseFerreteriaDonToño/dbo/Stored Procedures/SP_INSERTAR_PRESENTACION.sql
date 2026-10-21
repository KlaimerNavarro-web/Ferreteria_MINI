CREATE PROCEDURE [dbo].[SP_INSERTAR_PRESENTACION]
    @unidad_medida NVARCHAR(50),
	@nombre NVARCHAR(100),
    @peso DECIMAL(10, 3)
   
AS
BEGIN
    INSERT INTO Presentacion (unidad_medida, nombre, peso)
    VALUES (@unidad_medida, @nombre ,  @peso);
END;