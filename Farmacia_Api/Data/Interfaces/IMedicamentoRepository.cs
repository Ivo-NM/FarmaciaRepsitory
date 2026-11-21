using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IMedicamentoRepository
    {
        Task<Medicamento>? GetMedicamentoByIdAsync(int idMedicamento);
        Task<List<Medicamento>> GetMedicamentosAsync();
    }
}
