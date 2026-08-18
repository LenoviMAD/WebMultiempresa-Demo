using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _dbContext;

    public UsuarioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Usuario?> ObtenerPorEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Usuarios
            .AsNoTracking()
            .Include(u => u.Empresa)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int usuarioId, CancellationToken cancellationToken)
    {
        return await _dbContext.Usuarios
            .AsNoTracking()
            .Include(u => u.Empresa)
            .FirstOrDefaultAsync(u => u.UsuariosID == usuarioId, cancellationToken);
    }

    public async Task<IReadOnlyList<Usuario>> ListarAsync(int? empresaId, CancellationToken cancellationToken)
    {
        IQueryable<Usuario> query = _dbContext.Usuarios
            .AsNoTracking()
            .Include(u => u.Empresa)
            .Where(u => !u.Baja);

        if (empresaId.HasValue)
            query = query.Where(u => u.EmpresaID == empresaId);

        return await query.OrderBy(u => u.Nombre).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExisteEmailAsync(string email, int? excludeUsuariosID, CancellationToken cancellationToken)
    {
        return await _dbContext.Usuarios
            .AsNoTracking()
            .AnyAsync(u => u.Email == email.Trim().ToLowerInvariant()
                        && !u.Baja
                        && (excludeUsuariosID == null || u.UsuariosID != excludeUsuariosID),
                      cancellationToken);
    }

    public async Task<int> CrearAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        _dbContext.Usuarios.Add(usuario);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return usuario.UsuariosID;
    }

    public async Task ActualizarAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        _dbContext.Usuarios.Update(usuario);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BajaLogicaAsync(int usuariosId, CancellationToken cancellationToken)
    {
        await _dbContext.Usuarios
            .Where(u => u.UsuariosID == usuariosId)
            .ExecuteUpdateAsync(s => s.SetProperty(u => u.Baja, true), cancellationToken);
    }
}
