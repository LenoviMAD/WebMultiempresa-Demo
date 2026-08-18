CREATE OR ALTER PROCEDURE [dbo].[Pedidos_MMarcarDescargadosLote]
    @EmpresaID INT,
    @Ids       NVARCHAR(MAX)   -- JSON array de PedidosID: '[101,102,103]'
AS
BEGIN
    SET NOCOUNT ON;

    -- Marca como descargados los pedidos pendientes del lote.
    -- Solo afecta pedidos que aún no fueron cerrados (PedidoCerrado = 0).
    UPDATE p
    SET p.ResultadoTx   = 'Descargado',
        p.FechaTx       = GETUTCDATE(),
        p.PedidoCerrado = 1
    FROM dbo.Pedidos p
    INNER JOIN OPENJSON(@Ids) AS ids
        ON CAST(ids.[value] AS INT) = p.PedidosID
    WHERE p.EmpresaID    = @EmpresaID
      AND p.PedidoCerrado = 0
      AND p.Baja          = 0;

    SELECT @@ROWCOUNT AS FilasActualizadas;
END
