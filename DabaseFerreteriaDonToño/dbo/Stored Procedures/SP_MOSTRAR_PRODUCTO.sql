
CREATE PROCEDURE [dbo].[SP_MOSTRAR_PRODUCTO]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.id_producto,
        p.nombre,
        p.descripcion,
        i.stock_actual AS stock_final,
        p.id_categoria,
        c.nombre_categoria,
        p.id_presentacion,
        pr.unidad_medida,
        p.estado
    FROM dbo.Producto AS p
    INNER JOIN dbo.Categoria AS c
        ON p.id_categoria = c.id_categoria
    INNER JOIN dbo.Presentacion AS pr
        ON p.id_presentacion = pr.id_presentacion
    LEFT JOIN dbo.Inventario AS i
        ON p.id_producto = i.id_producto
    WHERE p.estado = 1
    ORDER BY p.id_producto DESC;
END;
