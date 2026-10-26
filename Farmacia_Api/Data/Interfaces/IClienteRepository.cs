using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync(Cliente cliente);
        Task<bool> Actualizar(Cliente cliente);
        Task<bool> Eliminar(Cliente cliente);
        
    }
}

