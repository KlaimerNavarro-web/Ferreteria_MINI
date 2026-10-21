CREATE PROCEDURE [dbo].[SP_INSERTAR_CATEGORIA]
    @nombre_categoria VARCHAR(50),
	@descripcion VARCHAR(100)

AS
BEGIN
    INSERT INTO Categoria (nombre_categoria,descripcion,estado)
    VALUES (@nombre_categoria,@descripcion,1)

    -- Devuelve el ID generado si usas una columna IDENTITY
    --SET @idcategoria = SCOPE_IDENTITY()
END
