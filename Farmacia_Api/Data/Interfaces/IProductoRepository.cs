using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IProductoRepository
    {
        Task<OtrosProducto>? GetProductoByIdAsync(int id);
    }
}
