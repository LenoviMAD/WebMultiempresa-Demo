-- Inserta la cabecera de un pedido y devuelve el PedidosID generado.
-- Las líneas de detalle se insertan con PedidosDetalle_ARegistrar en la misma transacción.
-- Reemplaza Pedidos_AgregarV2 (legacy) para empresas con EmpresaID > 0.
--
-- Idempotencia: si ya existe (EmpresaID, VendedoresID, PedidoCabeceraID):
--   YaExistia=1, YaDescargado=0  → pendiente, la app puede reintentar sin efecto
--   YaExistia=1, YaDescargado=1  → ya bajado por SincoApp, bloquear modificación
CREATE OR ALTER PROCEDURE Pedidos_ARegistrar
    @EmpresaID        INT,
    @VendedoresID     INT,
    @ClientesID       INT,
    @PedidoCabeceraID INT            = NULL,
    @DeviceID         NVARCHAR(100)  = NULL,
    @ClienteCodigo    NVARCHAR(50)   = NULL,
    @ClienteDireccion NVARCHAR(300)  = NULL,
    @Division         NVARCHAR(50)   = NULL,
    @Comentario       NVARCHAR(500)  = NULL,
    @FechaCarga       DATETIME2,
    @ConsumidorFinal  BIT            = 0,
    @TipoCliente      TINYINT        = 0,
    @Latitud          DECIMAL(18,10) = NULL,
    @Longitud         DECIMAL(18,10) = NULL,
    @TotalPedido      DECIMAL(18,2)  = 0,
    @EstadoArrastre   DECIMAL(18,2)  = 0,
    @FechaEntrega     DATETIME2      = NULL,
    @KeyPagoID        NVARCHAR(100)  = NULL,
    @PaymentID        NVARCHAR(100)  = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Idempotencia: devuelve el pedido existente sin reinsertar
    IF @PedidoCabeceraID IS NOT NULL
    BEGIN
        DECLARE @existente     INT;
        DECLARE @yaDescargado  BIT;

        SELECT @existente    = PedidosID,
               @yaDescargado = PedidoCerrado
        FROM   dbo.Pedidos
        WHERE  EmpresaID        = @EmpresaID
          AND  VendedoresID     = @VendedoresID
          AND  PedidoCabeceraID = @PedidoCabeceraID
          AND  Baja             = 0;

        IF @existente IS NOT NULL
        BEGIN
            SELECT @existente    AS PedidosID,
                   CAST(1 AS BIT) AS YaExistia,
                   @yaDescargado  AS YaDescargado;
            RETURN;
        END
    END

    INSERT INTO Pedidos
    (
        EmpresaID, VendedoresID, ClientesID,
        PedidoCabeceraID,
        DeviceID, ClienteCodigo, ClienteDireccion, Division, Comentario,
        FechaCarga, ConsumidorFinal, TipoCliente,
        Latitud, Longitud, TotalPedido, EstadoArrastre,
        FechaEntrega, KeyPagoID, PaymentID,
        PedidoCerrado, Baja, FechaCreacion
    )
    VALUES
    (
        @EmpresaID, @VendedoresID, @ClientesID,
        @PedidoCabeceraID,
        @DeviceID, @ClienteCodigo, @ClienteDireccion, @Division, @Comentario,
        @FechaCarga, @ConsumidorFinal, @TipoCliente,
        @Latitud, @Longitud, @TotalPedido, @EstadoArrastre,
        @FechaEntrega, @KeyPagoID, @PaymentID,
        0, 0, GETUTCDATE()
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS PedidosID,
           CAST(0 AS BIT)                AS YaExistia,
           CAST(0 AS BIT)                AS YaDescargado;
END
