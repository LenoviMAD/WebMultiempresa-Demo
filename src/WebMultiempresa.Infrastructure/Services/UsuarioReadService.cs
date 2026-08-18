using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Services;

public sealed class UsuarioReadService : IUsuarioReadPort
{
    private readonly AppDbContext _dbContext;
    private readonly ICurrentEmpresaContext _empresaContext;

    public UsuarioReadService(AppDbContext dbContext, ICurrentEmpresaContext empresaContext)
    {
        _dbContext = dbContext;
        _empresaContext = empresaContext;
    }

    public async Task<IReadOnlyList<UsuarioListadoDto>> ListarAsync(CancellationToken cancellationToken)
    {
        int? empresaId = _empresaContext.EmpresaID;

        IQueryable<Usuario> query = _dbContext.Usuarios
            .AsNoTracking()
            .Include(u => u.Empresa)
            .Where(u => !u.Baja);

        if (empresaId.HasValue)
            query = query.Where(u => u.EmpresaID == empresaId);

        return await query
            .OrderBy(u => u.Nombre)
            .Select(u => new UsuarioListadoDto(
                u.UsuariosID,
                u.EmpresaID,
                u.Empresa != null ? u.Empresa.Nombre : null,
                u.Email,
                u.Nombre,
                u.Rol,
                u.Baja,
                u.FechaCreacion))
            .ToListAsync(cancellationToken);
    }
}
