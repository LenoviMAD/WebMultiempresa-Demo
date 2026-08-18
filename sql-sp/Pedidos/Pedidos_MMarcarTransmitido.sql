-- Marca un pedido como transmitido al ERP y registra el resultado.
-- Llamado después de que el sistema central procesa el pedido.
CREATE OR ALTER PROCEDURE Pedidos_MMarcarTransmitido
    @PedidosID    INT,
    @EmpresaID    INT,
    @ResultadoTx  NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Pedidos
    SET    ResultadoTx  = @ResultadoTx,
           FechaTx      = GETUTCDATE(),
           PedidoCerrado = 1
    WHERE  PedidosID   = @PedidosID
      AND  EmpresaID   = @EmpresaID;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
