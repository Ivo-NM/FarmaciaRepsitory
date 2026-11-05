using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> GetAllAsync();
        Task<IEnumerable<Factura?>> GetAllInSedeAsync(int id);
        Task<Factura?> GetByIdAsync(int id);
        Task<List<Factura?>> GetByDateAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<bool> AddAsync(Factura factura);
        Task<Sucursale>? GetSedeByIdAsync(int id);
        Task<Empleado>? GetEmpleadoByIdAsync(int id);
        Task<Cliente>? GetClienteByIdAsync(int id);
        Task<FormasDePago>? GetFormaDePagoByIdAsync(int id);
    }
}
