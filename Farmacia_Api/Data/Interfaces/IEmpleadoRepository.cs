using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<Empleado>? GetByIdAsync(int id);
    }
}
