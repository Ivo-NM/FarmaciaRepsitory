using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly FarmaciaDbContext _context;
        public ClienteRepository(FarmaciaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .Include(c => c.ObraSocialNavigation)
                .Include(c => c.BarrioNavigation)
                .ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes
               .Include(c => c.ObraSocialNavigation)
               .Include(c => c.BarrioNavigation)
               .FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        public async Task<bool> UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
