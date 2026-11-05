using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IEntregaRepository
    {
        Task<IEnumerable<Entrega>> GetAllAsync();
        Task<IEnumerable<Entrega>> GetAllInSedeAsync(int id);
        Task<Entrega?> GetByIdAsync(int id);
        Task<bool> AddAsync(Entrega entrega);
    }
}
