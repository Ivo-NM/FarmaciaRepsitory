using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> LoginUsuarioAsync(Usuario usuario);
    }
}
