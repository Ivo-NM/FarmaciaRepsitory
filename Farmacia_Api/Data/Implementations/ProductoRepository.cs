using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Implementations
{
    public class ProductoRepository: IProductoRepository
    {
        private readonly FarmaciaDbContext context;

        public ProductoRepository(FarmaciaDbContext context)
        {
            this.context = context;
        }

        public Task<List<OtrosProducto?>> GetByMonth(int mesInicio, int mesFin)
        {
            return
        }
    }
}
