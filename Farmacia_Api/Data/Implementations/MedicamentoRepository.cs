using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class MedicamentoRepository: IMedicamentoRepository
    {
        private readonly FarmaciaDbContext _context;

        public MedicamentoRepository(FarmaciaDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Medicamento>? GetMedicamentoByIdAsync(int idMedicamento)
        {
            return await _context.Medicamentos.FirstOrDefaultAsync(m => m.CodMedicamento == idMedicamento);
        }
    }
}
