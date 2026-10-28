using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

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
                .Include(i => i.DetalleInventarios)
                .ThenInclude(di => di.CodMedicamentoNavigation)
                .Include(i => i.DetalleInventarios)
                .ThenInclude(di => di.CodProductoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<bool> RestarStockPorVentaAsync(int sucursalId, int InsumoId, int cantidadVendida)
        {
            // 1. Buscar el detalle de inventario de la sucursal que coincida con el medicamento o el producto
            var detalleInventario = await _context.DetalleInventarios
                .Include(di => di.NroInventarioNavigation) // Asumiendo que existe esta navegación al inventario padre
                .Where(di => di.NroInventarioNavigation.NroSucursal == sucursalId &&
                             (di.CodMedicamento == InsumoId || di.CodProducto == InsumoId))
                .FirstOrDefaultAsync();

            // 2. Validar si el registro existe
            if (detalleInventario == null)
            {
                return false; // El insumo no se encuentra en el inventario de esta sucursal
            }

            // 3. Validar si hay stock suficiente para cubrir la venta
            if (detalleInventario.Stock < cantidadVendida)
            {
                return false; // Stock insuficiente
            }

            // 4. Restar la cantidad vendida al stock actual
            detalleInventario.Stock -= cantidadVendida;

            // 5. Guardar los cambios en la base de datos
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SumarStockPorEntregaAsync(int sucursalId, int InsumoId, int cantidadRecibida)
        {
            // 1. Buscar el detalle de inventario de la sucursal que coincida con el medicamento o producto
            var detalleInventario = await _context.DetalleInventarios
                .Include(di => di.NroInventarioNavigation)
                .Where(di => di.NroInventarioNavigation.NroSucursal == sucursalId &&
                             (di.CodMedicamento == InsumoId || di.CodProducto == InsumoId))
                .FirstOrDefaultAsync();

            // 2. Validar si el registro ya existe en el inventario de esa sucursal
            if (detalleInventario == null)
            {
                // Si el insumo aún no está registrado en el inventario de la sucursal,
                // aquí podrías optar por crearlo o retornar false según la regla de negocio.
                return false;
            }

            // 3. Sumar la cantidad recibida al stock actual
            detalleInventario.Stock += cantidadRecibida;

            // 4. Guardar los cambios en la base de datos
            return await _context.SaveChangesAsync() > 0;
        }



        public Task<bool> VerificarStockDisponibleAsync(int sucursalId, int InsumoId, int cantidadRequerida)
        {
            throw new NotImplementedException();
        }
    }
}
