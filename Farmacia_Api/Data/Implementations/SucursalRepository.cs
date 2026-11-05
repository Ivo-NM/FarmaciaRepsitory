using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class SucursalRepository: ISucursalRepository
    {
        private readonly FarmaciaDbContext _context;

        public SucursalRepository(FarmaciaDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Sucursale>? GetByIdAsync(int idSucursal)
        {
            return await _context.Sucursales.FirstOrDefaultAsync(s => s.NroSucursal == idSucursal);
        }
    }
}
