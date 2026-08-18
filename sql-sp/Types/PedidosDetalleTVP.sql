-- Tipo tabla para inserción masiva de líneas de pedido.
-- Ejecutar una sola vez por BD; el SP PedidosDetalle_ARegistrar depende de este tipo.
IF TYPE_ID(N'dbo.PedidosDetalleTVP') IS NULL
BEGIN
    CREATE TYPE dbo.PedidosDetalleTVP AS TABLE
    (
        ProductosID      INT              NOT NULL,
        Cantidad         DECIMAL(18,3)    NOT NULL,
        Precio           DECIMAL(18,4)    NOT NULL,
        ListasPreciosID  INT              NOT NULL,
        Descuento1       DECIMAL(5,2)     NOT NULL DEFAULT 0,
        Descuento2       DECIMAL(5,2)     NOT NULL DEFAULT 0,
        EsCombo          BIT              NOT NULL DEFAULT 0,
        CombosID         INT              NULL,
        TipoComboItem    TINYINT          NULL,
        UnidadesPorBulto DECIMAL(18,3)    NULL,
        GrupoDinamico    INT              NULL
    );
END
