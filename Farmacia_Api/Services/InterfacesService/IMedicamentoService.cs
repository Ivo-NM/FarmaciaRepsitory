using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IMedicamentoService
    {
        Task<Medicamento>? ObtenerMedicamentoPorIdAsync(int idMedicamento);
        Task<List<Medicamento>> ObtenerMedicamentosAsync();
    }
}
