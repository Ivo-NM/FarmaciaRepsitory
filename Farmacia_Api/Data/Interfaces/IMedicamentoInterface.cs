using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IMedicamentoInterface
    {
        Task<Medicamento>? GetMedicamentoById(int idMedicamento);
    }
}
