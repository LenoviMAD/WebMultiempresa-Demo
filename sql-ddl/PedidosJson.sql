-- Archivo del JSON crudo de cada pedido recibido desde la app para empresas multiempresa.
-- Equivalente a PedidosJsonTx de la legacy. Permite auditoría y reenvío manual.
CREATE TABLE PedidosJson (
    PedidosJsonID  BIGINT        IDENTITY(1,1) NOT NULL,
    EmpresaID      INT           NOT NULL,
    PedidosID      INT           NOT NULL,          -- FK a Pedidos (0 si el pedido no se grabó)
    VendedoresID   INT           NOT NULL,
    ClientesID     INT           NOT NULL,
    FechaCarga     DATETIME2     NOT NULL DEFAULT GETUTCDATE(),
    JsonPedido     NVARCHAR(MAX) NOT NULL,
    Baja           BIT           NOT NULL DEFAULT 0,

    CONSTRAINT PK_PedidosJson PRIMARY KEY (PedidosJsonID),
    CONSTRAINT FK_PedidosJson_Empresas FOREIGN KEY (EmpresaID) REFERENCES Empresas (EmpresaID)
);

CREATE INDEX IX_PedidosJson_EmpresaID_FechaCarga ON PedidosJson (EmpresaID, FechaCarga DESC);
CREATE INDEX IX_PedidosJson_PedidosID            ON PedidosJson (PedidosID);
