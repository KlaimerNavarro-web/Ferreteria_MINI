
CREATE PROCEDURE [dbo].[SP_BUSCAR_PRODUCTO]
    @textobuscar NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.id_producto,
        p.nombre,
        p.descripcion,
        df.precio_venta AS precio,
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
    LEFT JOIN dbo.Detalle_Factura AS df
        ON p.id_producto = df.id_producto
    WHERE p.estado = 1
      AND p.nombre LIKE '%' + @textobuscar + '%'
    ORDER BY p.nombre;
END;
