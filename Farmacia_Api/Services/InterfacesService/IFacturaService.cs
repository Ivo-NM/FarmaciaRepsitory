using Farmacia_Api.Dtos.FacturasDto;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IFacturaService
    {
        Task<IEnumerable<ReadFacturaDto>> ObtenerFacturasAsync();
        Task<IEnumerable<ReadFacturaDto?>> ObtenerFacturasPorSucursalAsync(int id);
        Task<ReadFacturaDto?> ObtenerFacturasPorIdAsync(int id);
        Task<List<ReadFacturaDto>> ObtenerFacturasPorFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<bool> CrearFacturaAsync(CreateFacturaDto factura);
        Task<Cliente>? ObtenerClientePorIdAsync(int id);
        Task<FormasDePago> ObtenerFormaDePagoPorIdAsync(int id);
    }
}
