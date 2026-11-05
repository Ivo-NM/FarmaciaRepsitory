using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class EntregaRepository : IEntregaRepository
    {
        private readonly FarmaciaDbContext _context;
        public EntregaRepository(FarmaciaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Entrega entrega)
        {
            await _context.Entregas.AddAsync(entrega);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Entrega>> GetAllInSedeAsync(int id)
        {
            return await _context.Entregas
                .Where(e => e.NroSucursal == id)
                .Include(e => e.DetalleEntregas)
                .ThenInclude(de => de.CodMedicamentoNavigation)
                .Include(e => e.DetalleEntregas)
                .ThenInclude(de => de.CodProductoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Entrega>> GetAllAsync()
        {
            return await _context.Entregas
                .Include(e => e.DetalleEntregas)
                  .ThenInclude(de => de.CodMedicamentoNavigation)
                .Include(e => e.DetalleEntregas)
                  .ThenInclude(de => de.CodProductoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Entrega?> GetByIdAsync(int id)
        {
            return await _context.Entregas
                .Where(e => e.NroEntrega == id)
                .Include(e => e.DetalleEntregas)
                .ThenInclude(de => de.CodMedicamentoNavigation)
                .Include(e => e.DetalleEntregas)
                .ThenInclude(de => de.CodProductoNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Sucursale>? GetSucursalesByIdAsync(int idSucursal)
        {
            return await _context.Sucursales.FirstOrDefaultAsync(s => s.NroSucursal == idSucursal);
        }

        public async Task<Empleado>? GetEmpleadoByIdAsync(int idEmpleado)
        {
            return await _context.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == idEmpleado);
        }
    }
}
