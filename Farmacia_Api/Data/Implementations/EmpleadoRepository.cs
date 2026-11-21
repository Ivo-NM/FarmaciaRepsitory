using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class EmpleadoRepository: IEmpleadoRepository
    {
        private readonly FarmaciaDbContext _context;

        public EmpleadoRepository(FarmaciaDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Empleado>? GetByIdandSedeAsync(int id, int idsucursal)
        {
            return await _context.Empleados
                .FirstOrDefaultAsync(e => e.IdEmpleado == id && e.NroSucursal == idsucursal);
        }

        public async Task<Empleado>? GetByIdAsync(int id)
        {
            return await _context.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
        }

        public async Task<List<Empleado>> GetBySedeAsync(int idSucursal)
        {
            return await _context.Empleados
                         .Where(e => e.NroSucursal == idSucursal)
                         .ToListAsync();
        }
    }
}
