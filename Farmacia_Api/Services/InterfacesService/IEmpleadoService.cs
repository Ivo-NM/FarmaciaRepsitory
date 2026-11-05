using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IEmpleadoService
    {
        Task<Empleado>? ObtenerEmpleadoPorIdAsync(int id);
    }
}
