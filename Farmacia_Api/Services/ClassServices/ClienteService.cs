using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.ClientesDto;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class ClienteService: IClienteService
    {
        private readonly IClienteRepository repository;

        public ClienteService(IClienteRepository repository)
        {
            this.repository = repository;
        }

        public Task<bool> ActualizarClienteAsync(UpdateClienteDto clienteDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BorrarClienteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CrearClienteAsync(CreateClienteDto clienteDto)
        {
            throw new NotImplementedException();
        }

        public Task<ReadClienteDto?> RecuperarClientePorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReadClienteDto>> RecuperarClientesAsync()
        {
            List<Cliente> clientes = (await repository.GetAllAsync()).ToList();
            List<ReadClienteDto> lstClienteDto = Mapeo(clientes);
            return lstClienteDto;
        }

        public List<ReadClienteDto> Mapeo(List<Cliente> clientes)
        {
            List<ReadClienteDto> lstClienteDto = new List<ReadClienteDto>();

            foreach (Cliente cliente in clientes)
            {
                ReadClienteDto clienteDto = new ReadClienteDto()
                {
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Documento = cliente.Documento,
                    NroTelefono = cliente.NroTelefono,
                    Email = cliente.EMail,
                    Barrio = cliente.BarrioNavigation.NombreBarrio,
                    ObraSocialNombre = cliente.ObraSocialNavigation != null ? cliente.ObraSocialNavigation.Detalle : string.Empty
                };

                lstClienteDto.Add(clienteDto);
            }

            return lstClienteDto;
        }
    }
}
