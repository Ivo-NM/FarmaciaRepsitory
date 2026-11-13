using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IEmpleadoService
    {
        Task<Empleado>? ObtenerEmpleadoPorIdAsync(int id);
        Task<Empleado>? ObtenerEmpleadoPorIdySedeAsync(int id, int idsucursal);
        Task<List<Empleado>> ObtenerPorSedeAsync(int idSucursal);
    }
}
