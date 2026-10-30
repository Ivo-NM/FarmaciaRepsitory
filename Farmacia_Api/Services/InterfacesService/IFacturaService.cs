using Farmacia_Api.Dtos;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IFacturaService
    {
        Task<IEnumerable<ReadFacturaDto>> ObtenerFacturasAsync();
        Task<IEnumerable<ReadFacturaDto>> ObtenerFacturasPorSucursalAsync(int id);
        Task<ReadFacturaDto?> ObtenerFacturasPorIdAsync(int id);
        Task<bool> CrearFacturaAsync(CreateFacturaDto factura);
    }
}
