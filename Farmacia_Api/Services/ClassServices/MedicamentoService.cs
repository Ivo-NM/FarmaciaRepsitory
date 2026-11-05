using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class MedicamentoService: IMedicamentoService
    {
        private readonly IMedicamentoRepository repository;

        public MedicamentoService(IMedicamentoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Medicamento>? ObtenerMedicamentoPorIdAsync(int idMedicamento)
        {
            return await repository.GetMedicamentoByIdAsync(idMedicamento);
        }
    }
}
