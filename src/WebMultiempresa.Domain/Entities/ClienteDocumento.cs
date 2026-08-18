namespace WebMultiempresa.Domain.Entities;

public sealed class ClienteDocumento
{
    public int ClienteDocumentosID { get; private set; }
    public int ClientesID { get; private set; }
    public int EmpresaID { get; private set; }
    public int TiposDocumentosID { get; private set; }
    public string Numero { get; private set; } = string.Empty;
    public bool Validado { get; private set; }
    public bool Baja { get; private set; }

    private ClienteDocumento() { }

    public static ClienteDocumento Crear(
        int clientesId,
        int empresaId,
        int tiposDocumentosId,
        string numero) =>
        new()
        {
            ClientesID        = clientesId,
            EmpresaID         = empresaId,
            TiposDocumentosID = tiposDocumentosId,
            Numero            = numero.Trim(),
            Validado          = false,
            Baja              = false,
        };

    public void ActualizarNumero(string numero) => Numero = numero.Trim();
    public void Validar() => Validado = true;
    public void DarDeBaja() => Baja = true;
}
