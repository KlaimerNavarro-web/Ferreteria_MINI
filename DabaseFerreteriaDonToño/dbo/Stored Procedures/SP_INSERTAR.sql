CREATE PROCEDURE [dbo].[SP_INSERTAR]
    @idcategoria INT OUTPUT,
    @nombre VARCHAR(50)
AS
BEGIN
    INSERT INTO Categoria (nombre_categoria)
    VALUES (@nombre)

    -- Devuelve el ID generado si usas una columna IDENTITY
    SET @idcategoria = SCOPE_IDENTITY()
END
