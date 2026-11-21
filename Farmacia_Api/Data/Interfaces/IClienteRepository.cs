using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<bool> AddAsync(Cliente cliente);
        Task<bool> UpdateAsync(Cliente cliente);
        Task<bool> DeleteAsync(int id);
        Task<Barrio>? RecuperarBarrioAsync(int idBarrio);
        Task<ObrasSociale>? RecuperarObraSocialAsync(int idObraSocial);
    }
}
