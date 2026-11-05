using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface ISucursalRepository
    {
        Task<Sucursale>? GetByIdAsync(int idSucursal);
    }
}
