CREATE TYPE [dbo].[EDETALLE_COMPRA] AS TABLE (
    [id_producto]   INT             NULL,
    [precio_compra] DECIMAL (18, 2) NULL,
    [precio_venta]  DECIMAL (18, 2) NULL,
    [iva]           DECIMAL (18, 2) NULL,
    [subtotal]      DECIMAL (18, 2) NULL,
    [cantidad]      VARCHAR (100)   NULL);

