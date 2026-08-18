// src/WebMultiempresa.Infrastructure/Services/BlazorCurrentEmpresaContext.cs
using WebMultiempresa.Application.Ports;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Services;

/// <summary>
/// Implementa ICurrentEmpresaContext para Blazor Server.
/// Lee EmpresaID desde EmpresaContexto (scoped), que se llena al hacer login.
/// </summary>
public sealed class BlazorCurrentEmpresaContext : ICurrentEmpresaContext
{
    private readonly EmpresaContexto _empresaContexto;

    public BlazorCurrentEmpresaContext(EmpresaContexto empresaContexto)
    {
        _empresaContexto = empresaContexto;
    }

    public int? EmpresaID => _empresaContexto.EmpresaID;
}
