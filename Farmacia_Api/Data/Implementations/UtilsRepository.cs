using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class UtilsRepository : IUtilsRepository
    {
        private readonly FarmaciaDbContext _context;
        public UtilsRepository(FarmaciaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Barrio>> GetAllBarriosAsync()
        {
            return await _context.Barrios.ToListAsync();
        }

        public async Task<List<ObrasSociale>> GetAllObraSocialAsync()
        {
            return await _context.ObrasSociales.ToListAsync();
        }

        public async Task<Barrio> GetBarriosByAsync(int id)
        {
            return await _context.Barrios.FirstOrDefaultAsync(b => b.IdBarrio == id);
        }

        public async Task<ObrasSociale> GetObraSocialByAsync(int id)
        {
            return await _context.ObrasSociales.FirstOrDefaultAsync(o => o.CodObraSocial == id);
        }
    }
}
