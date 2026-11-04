using Farmacia_Api.Dtos.ClientesDto;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IClienteService
    {
        Task<IEnumerable<ReadClienteDto>> RecuperarClientesAsync();
        Task<ReadClienteDto?> RecuperarClientePorIdAsync(int id);
        Task<bool> CrearClienteAsync(Cliente cliente);
        Task<bool> ActualizarClienteAsync(Cliente cliente);
        Task<bool> BorrarClienteAsync(int id);
    }
}
