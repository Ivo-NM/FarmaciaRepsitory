using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IUtilsRepository
    {
        Task<List<Barrio>> GetAllBarriosAsync();
        Task<Barrio> GetBarriosByAsync(int id);
        Task<List<ObrasSociale>> GetAllObraSocialAsync();
        Task<ObrasSociale> GetObraSocialByAsync(int id);
        Task<List<FormasDePago>> GetFormasPagoAsync();
    }
}
