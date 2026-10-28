using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IFacturaService
    {
        Task<IEnumerable<Factura>> ObtenerFacturasAsync();
        Task<IEnumerable<Factura>> ObtenerFacturasPorSucursalAsync(int id);
        Task<Factura?> ObtenerFacturasPorIdAsync(int id);
        Task<bool> CrearFacturaAsync(Factura factura);
    }
}
