-- Archiva el JSON crudo del pedido. Se llama después de Pedidos_ARegistrar.
-- Si falla no debe afectar al pedido ya guardado (se invoca fuera de la transacción).
CREATE OR ALTER PROCEDURE PedidosJson_ARegistrar
    @EmpresaID   INT,
    @PedidosID   INT,
    @VendedoresID INT,
    @ClientesID  INT,
    @JsonPedido  NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO PedidosJson (EmpresaID, PedidosID, VendedoresID, ClientesID, JsonPedido)
    VALUES (@EmpresaID, @PedidosID, @VendedoresID, @ClientesID, @JsonPedido);
END
