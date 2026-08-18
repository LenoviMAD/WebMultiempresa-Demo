namespace WebMultiempresa.Domain.Entities;

public sealed class Pedido
{
    public int PedidosID { get; private set; }
    public int EmpresaID { get; private set; }
    public int VendedoresID { get; private set; }
    public int ClientesID { get; private set; }
    public string? DeviceID { get; private set; }
    public string? ClienteCodigo { get; private set; }
    public string? ClienteDireccion { get; private set; }
    public string? Division { get; private set; }
    public string? Comentario { get; private set; }
    public DateTime FechaCarga { get; private set; }
    public DateTime? FechaTx { get; private set; }
    public decimal TotalPedido { get; private set; }
    public decimal EstadoArrastre { get; private set; }
    public bool ConsumidorFinal { get; private set; }
    public byte TipoCliente { get; private set; }
    public decimal? Latitud { get; private set; }
    public decimal? Longitud { get; private set; }
    public string? ResultadoTx { get; private set; }
    public bool PedidoCerrado { get; private set; }
    public DateTime? FechaEntrega { get; private set; }
    public string? KeyPagoID { get; private set; }
    public string? PaymentID { get; private set; }
    public bool Baja { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private Pedido() { }

    public static Pedido Crear(
        int empresaId,
        int vendedoresId,
        int clientesId,
        DateTime fechaCarga,
        decimal totalPedido,
        bool consumidorFinal,
        byte tipoCliente,
        string? deviceId = null,
        string? clienteCodigo = null,
        string? clienteDireccion = null,
        string? division = null,
        string? comentario = null,
        decimal estadoArrastre = 0m,
        decimal? latitud = null,
        decimal? longitud = null,
        DateTime? fechaEntrega = null,
        string? keyPagoId = null,
        string? paymentId = null) =>
        new()
        {
            EmpresaID        = empresaId,
            VendedoresID     = vendedoresId,
            ClientesID       = clientesId,
            FechaCarga       = fechaCarga,
            TotalPedido      = totalPedido,
            ConsumidorFinal  = consumidorFinal,
            TipoCliente      = tipoCliente,
            DeviceID         = deviceId?.Trim(),
            ClienteCodigo    = clienteCodigo?.Trim(),
            ClienteDireccion = clienteDireccion?.Trim(),
            Division         = division?.Trim(),
            Comentario       = comentario?.Trim(),
            EstadoArrastre   = estadoArrastre,
            Latitud          = latitud,
            Longitud         = longitud,
            FechaEntrega     = fechaEntrega,
            KeyPagoID        = keyPagoId?.Trim(),
            PaymentID        = paymentId?.Trim(),
            PedidoCerrado    = false,
            Baja             = false,
            FechaCreacion    = DateTime.UtcNow
        };

    public void MarcarTransmitido(string resultadoTx)
    {
        ResultadoTx   = resultadoTx;
        FechaTx       = DateTime.UtcNow;
        PedidoCerrado = true;
    }

    public void DarDeBaja() => Baja = true;
}
