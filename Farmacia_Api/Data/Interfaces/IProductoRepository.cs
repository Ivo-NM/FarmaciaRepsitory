using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<OtrosProducto?>> GetVendidosByFecha(DateTime fechaInicio, DateTime fechaFin);
    }
}
