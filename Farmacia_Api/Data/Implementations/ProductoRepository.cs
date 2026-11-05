using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class ProductoRepository: IProductoRepository
    {
        private readonly FarmaciaDbContext _context;

        public ProductoRepository(FarmaciaDbContext _context)
        {
            this._context = _context;
        }

        public async Task<OtrosProducto>? GetProductoByIdAsync(int id)
        {
            return await _context.OtrosProductos.FirstOrDefaultAsync(p => p.CodProducto == id);
        }
    }
}
