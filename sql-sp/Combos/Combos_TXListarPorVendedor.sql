-- Retorna filas compatibles con la app móvil de ventas (una fila por producto por combo).
-- @FechaDesde NULL = traer todos; != NULL = solo combos con vigencia posterior a esa fecha.
-- @ClienteID  0   = sin filtro de lista; != 0 = usa lista de precio del cliente.
CREATE OR ALTER PROCEDURE [dbo].[Combos_TXListarPorVendedor]
    @VendedoresID INT,
    @FechaDesde   DATETIME2 = NULL,
    @ClienteID    INT       = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EmpresaID INT;
    SELECT @EmpresaID = [EmpresaID]
    FROM   [dbo].[Vendedores]
    WHERE  [VendedoresID] = @VendedoresID;

    IF @EmpresaID IS NULL RETURN;

    -- Busca la lista principal del cliente en ClienteListasPrecios; fallback a 1.
    DECLARE @ListasPreciosID INT = 1;
    IF @ClienteID > 0
        SELECT TOP 1 @ListasPreciosID = ISNULL([ListasPreciosID], 1)
        FROM   [dbo].[ClienteListasPrecios]
        WHERE  [ClientesID] = @ClienteID
          AND  [EmpresaID]  = @EmpresaID
          AND  [EsPrincipal] = 1
          AND  [Baja]        = 0;

    SELECT
        c.[CombosID]                               AS [comb_combosid],
        c.[Nombre]                                 AS [comb_nombre],
        c.[Codigo]                                 AS [comb_codigo],
        c.[CupoTotal]                              AS [comb_cantmax],
        c.[CantidadDinamica]                       AS [comb_CantDinam],
        c.[CantidadSinCargo]                       AS [comb_CantSCargo],
        c.[ImporteProductosFuera]                  AS [comb_ImporteProdFComb],
        c.[MarcasProductosID]                      AS [comb_RubroComb],
        c.[CantidadPorFactura]                     AS [comb_CantMaxXFact],
        CAST(c.[EsDeIntroduccion] AS INT)          AS [comb_Intro],
        ci.[ComboItemsID]                          AS [comb_dpcID],
        ci.[ProductosID]                           AS [comb_productosid],
        ci.[Cantidad]                              AS [comb_cantidad],
        ISNULL(pp.[PrecioFinal], 0)                AS [comb_precio],
        ci.[Tipo]                                  AS [comb_tipo],
        ci.[Descuento1]                            AS [comb_descuento1],
        ci.[Descuento2]                            AS [comb_descuento2],
        ci.[NroGrupoDinamico]                      AS [comb_NrGpDin]
    FROM  [dbo].[Combos] c
    INNER JOIN [dbo].[ComboItems] ci
           ON  ci.[CombosID]    = c.[CombosID]
    INNER JOIN [dbo].[Productos] p
           ON  p.[ProductosID]  = ci.[ProductosID]
           AND p.[EmpresaID]    = @EmpresaID
    LEFT JOIN  [dbo].[ProductoPrecios] pp
           ON  pp.[ProductosID]     = p.[ProductosID]
           AND pp.[ListasPreciosID] = @ListasPreciosID
    WHERE  c.[EmpresaID] = @EmpresaID
      AND  c.[Baja] = 0
      AND  ISNULL(p.[DesactivadoParaLaVenta], 0) = 0
      AND  ISNULL(p.[Baja], 0) = 0
      AND  (
               @FechaDesde IS NULL
            OR c.[FechaVigencia] >= @FechaDesde
            OR EXISTS (
                   SELECT 1
                   FROM   [dbo].[ComboFechas] cf
                   WHERE  cf.[CombosID]    = c.[CombosID]
                     AND  cf.[FechaFinal] >= @FechaDesde
               )
           )
    ORDER BY c.[CombosID],
             ci.[NroGrupoDinamico],
             p.[ProductosID];
END
GO
