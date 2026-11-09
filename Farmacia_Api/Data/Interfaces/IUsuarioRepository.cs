using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> LoginUsuarioAsync(Usuario usuario);
    }
}
