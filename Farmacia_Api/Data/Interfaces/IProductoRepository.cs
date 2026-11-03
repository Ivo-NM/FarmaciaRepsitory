using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<OtrosProducto?>> GetByMonth(int mesInicio, int mesFin);
    }
}
