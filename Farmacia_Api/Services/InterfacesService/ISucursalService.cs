using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface ISucursalService
    {
        Task<Sucursale>? ObtenerSucursalPorId(int idSucursal);
        Task<List<Sucursale>> ObtenerSucursales();
    }
}
