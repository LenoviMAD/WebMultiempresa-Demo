-- Pedidos pendientes de transmisión de un vendedor. Usado por la app para
-- sincronizar pedidos al reconectarse después de trabajar offline.
CREATE OR ALTER PROCEDURE Pedidos_TXListarPorVendedor
    @VendedoresID  INT,
    @EmpresaID     INT,
    @SoloPendientes BIT = 1   -- 1 = solo PedidoCerrado=0, 0 = todos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.PedidosID,
        p.VendedoresID,
        p.ClientesID,
        p.DeviceID,
        p.ClienteCodigo,
        p.ClienteDireccion,
        p.Division,
        p.Comentario,
        p.FechaCarga,
        p.FechaTx,
        p.TotalPedido,
        p.EstadoArrastre,
        p.ConsumidorFinal,
        p.TipoCliente,
        p.Latitud,
        p.Longitud,
        p.ResultadoTx,
        p.PedidoCerrado,
        p.FechaEntrega
    FROM Pedidos p
    WHERE p.VendedoresID  = @VendedoresID
      AND p.EmpresaID     = @EmpresaID
      AND p.Baja          = 0
      AND (@SoloPendientes = 0 OR p.PedidoCerrado = 0)
    ORDER BY p.FechaCarga DESC;
END
