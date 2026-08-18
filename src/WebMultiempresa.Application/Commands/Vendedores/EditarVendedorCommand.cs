namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class EditarVendedorCommand
{
    public int VendedoresID { get; init; }
    public string Codigo { get; init; } = string.Empty;
    public string Nombre { get; init; } = string.Empty;
    public string? WhatsApp { get; init; }
    public int? CategoriasComercialesID { get; init; }
    public bool VerTodasLasCategorias { get; init; }
    public string? Clave { get; init; }
    public int? TiposDeVendedoresID { get; init; }
    public int DiaInicioRuta { get; init; }
}
