-- Agrega PedidoCabeceraID a Pedidos para soportar idempotencia en PostMultiempresa.
-- PedidoCabeceraID es el ID local del pedido en el SQLite de la app del vendedor.
-- La combinación (EmpresaID, VendedoresID, PedidoCabeceraID) debe ser única.
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.Pedidos')
      AND name       = N'PedidoCabeceraID'
)
BEGIN
    ALTER TABLE dbo.Pedidos ADD PedidoCabeceraID INT NULL;
END

-- El índice se crea con EXEC para que SQL Server valide la columna en tiempo de ejecución
-- y no en tiempo de parseo del batch (evita error 207 "Invalid column name").
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Pedidos')
      AND name       = N'UQ_Pedidos_Idempotencia'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX UQ_Pedidos_Idempotencia
               ON dbo.Pedidos (EmpresaID, VendedoresID, PedidoCabeceraID)
               WHERE PedidoCabeceraID IS NOT NULL;');
END
