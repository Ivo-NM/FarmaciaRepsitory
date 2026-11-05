using Farmacia_Api.Dtos.ClientesDto;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IClienteService
    {
        Task<IEnumerable<ReadClienteDto>> RecuperarClientesAsync();
        Task<ReadClienteDto?> RecuperarClientePorIdAsync(int id);
        Task<bool> CrearClienteAsync(CreateClienteDto clienteDto);
        Task<bool> ActualizarClienteAsync(UpdateClienteDto clienteDto, int id);
        Task<bool> BorrarClienteAsync(int id);

        Task<Barrio>? RecuperarBarrioAsync(int idBarrio);
        Task<ObrasSociale>? RecuperarObraSocialAsync(int idObraSocial);
    }
}
