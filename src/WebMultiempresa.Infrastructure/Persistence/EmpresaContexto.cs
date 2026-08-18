namespace WebMultiempresa.Infrastructure.Persistence;

/// <summary>
/// Servicio scoped que almacena el EmpresaID del usuario autenticado.
/// Se inyecta en AppDbContext para aplicar Global Query Filters.
/// </summary>
public sealed class EmpresaContexto
{
    public int? EmpresaID { get; set; }
}
