using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorEmailAsync(string email, CancellationToken cancellationToken);
    Task<Usuario?> ObtenerPorIdAsync(int usuarioId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Usuario>> ListarAsync(int? empresaId, CancellationToken cancellationToken);
    Task<bool> ExisteEmailAsync(string email, int? excludeUsuariosID, CancellationToken cancellationToken);
    Task<int> CrearAsync(Usuario usuario, CancellationToken cancellationToken);
    Task ActualizarAsync(Usuario usuario, CancellationToken cancellationToken);
    Task BajaLogicaAsync(int usuariosId, CancellationToken cancellationToken);
}
