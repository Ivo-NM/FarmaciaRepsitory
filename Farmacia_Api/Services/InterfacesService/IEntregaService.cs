using Farmacia_Api.Dtos;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IEntregaService
    {
        Task<IEnumerable<ReadEntregaDto>> RecuperarEntregasAsync();
        Task<IEnumerable<ReadEntregaDto>> RecuperarEntregasPorSedeAsync(int id);
        Task<ReadEntregaDto?> RecuperarEntregaPorIdAsync(int id);
        Task<bool> CrearEntregaAsync(Entrega entrega);
    }
}
