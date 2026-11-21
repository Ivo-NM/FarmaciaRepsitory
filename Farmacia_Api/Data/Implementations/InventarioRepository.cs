using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Farmacia_Api.Data.Implementations
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly FarmaciaDbContext _context;
        public InventarioRepository(FarmaciaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Inventario inventario)
        {
            await _context.Inventarios.AddAsync(inventario);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Inventario inventario)
        {
            _context.Inventarios.Update(inventario);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Inventario>> GetAllAsync()
        {
            return await _context.Inventarios
                .Include(i => i.NroSucursalNavigation)
                .Include(i => i.DetalleInventarios)
                .ThenInclude(di => di.CodMedicamentoNavigation)
                .Include(i => i.DetalleInventarios)
                .ThenInclude(di => di.CodProductoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Inventario?> GetByIdAsync(int id)
        {
            return await _context.Inventarios
                .Where(i => i.NroInventario == id)
                .Include(i => i.NroSucursalNavigation)
                .Include(i => i.DetalleInventarios)
                .ThenInclude(di => di.CodMedicamentoNavigation)
                .Include(i => i.DetalleInventarios)
                .ThenInclude(di => di.CodProductoNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Inventario>> GetByInSedeAsync(int id)
        {
            return await _context.Inventarios
                .Where(i => i.NroSucursal == id)
                .Include(i => i.NroSucursalNavigation)
                .Include(i => i.DetalleInventarios)
                .ThenInclude(di => di.CodMedicamentoNavigation)
                .Include(i => i.DetalleInventarios)
                .ThenInclude(di => di.CodProductoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<DetalleInventario?> VerificarStockDisponibleAsync(int sucursalId, int insumoId, int cantidadRequerida, bool esMedicamento)
        {
            // 1. Buscar el detalle de inventario de la sucursal
            var detalleInventario =  _context.DetalleInventarios
                .Include(di => di.NroInventarioNavigation)
                .Where(di => di.NroInventarioNavigation.NroSucursal == sucursalId);

            if (esMedicamento)
            {
                detalleInventario = detalleInventario.Where(di => di.CodMedicamento == insumoId);
            }
            else
            {
                detalleInventario = detalleInventario.Where(di => di.CodProducto == insumoId);
            }
            return await detalleInventario.FirstOrDefaultAsync();
        }

        public async Task<bool> ModificarStockAsync(DetalleInventario detalleInventario, int cantidad, bool esResta)
        {
            if (esResta == true)
            {
                cantidad = cantidad * -1;
            }
            detalleInventario.Stock += cantidad; 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
