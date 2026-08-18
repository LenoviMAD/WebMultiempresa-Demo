-- Inserta todas las líneas de un pedido en una sola operación usando TVP.
-- Requiere que el tipo dbo.PedidosDetalleTVP exista (ver sql-sp/Types/PedidosDetalleTVP.sql).
-- Llamar dentro de la misma transacción que Pedidos_ARegistrar.
CREATE OR ALTER PROCEDURE PedidosDetalle_ARegistrar
    @PedidosID  INT,
    @Items      dbo.PedidosDetalleTVP READONLY
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO PedidosDetalle
    (
        PedidosID, ProductosID, Cantidad, Precio, ListasPreciosID,
        Descuento1, Descuento2, EsCombo, CombosID, TipoComboItem,
        UnidadesPorBulto, GrupoDinamico
    )
    SELECT
        @PedidosID,
        ProductosID,
        Cantidad,
        Precio,
        ListasPreciosID,
        Descuento1,
        Descuento2,
        EsCombo,
        CombosID,
        TipoComboItem,
        UnidadesPorBulto,
        GrupoDinamico
    FROM @Items;

    SELECT @@ROWCOUNT AS FilasInsertadas;
END
