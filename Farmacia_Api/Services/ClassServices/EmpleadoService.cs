using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class EmpleadoService: IEmpleadoService
    {
        private readonly IEmpleadoRepository repository;

        public EmpleadoService(IEmpleadoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Empleado>? ObtenerEmpleadoPorIdySedeAsync(int id, int idsucursal)
        {
            return await repository.GetByIdandSedeAsync(id, idsucursal);
        }

        public async Task<Empleado>? ObtenerEmpleadoPorIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<List<Empleado>> ObtenerPorSedeAsync(int idSucursal)
        {
            return await repository.GetBySedeAsync(idSucursal);
        }
    }
}
