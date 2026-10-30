using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly FarmaciaDbContext _context;
        public FacturaRepository(FarmaciaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Factura>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(f => f.NroSucursalNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodMedicamentoNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodProductoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Factura>> GetAllInSedeAsync(int id)
        {
            return await _context.Facturas
                .Where(f => f.NroSucursal == id)
                .Include(f => f.NroSucursalNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodMedicamentoNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodProductoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.NroSucursalNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodMedicamentoNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodProductoNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync(f => f.NroFactura == id);

        }
    }
}
