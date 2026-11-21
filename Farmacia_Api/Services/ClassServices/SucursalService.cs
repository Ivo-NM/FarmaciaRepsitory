using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using System.Runtime.CompilerServices;

namespace Farmacia_Api.Services.ClassServices
{
    public class SucursalService: ISucursalService
    {
        private readonly ISucursalRepository repository;

        public SucursalService(ISucursalRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Sucursale>> ObtenerSucursales()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Sucursale>? ObtenerSucursalPorId(int idSucursal)
        {
            return await repository.GetByIdAsync(idSucursal);
        }
    }
}
