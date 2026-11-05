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

        public async Task<bool> ActualizarClienteAsync(UpdateClienteDto clienteDto, int id)
        {
            //No existe el barrio

            if (clienteDto.IdBarrio.HasValue)
            {
                Barrio? barrio = await RecuperarBarrioAsync(clienteDto.IdBarrio.Value);

                if (barrio == null)
                {
                    throw new ArgumentException("No existe el barrio");
                }
            }

            //No existe la obra social

            if (clienteDto.CodObraSocial.HasValue)
            {
                ObrasSociale? os = await RecuperarObraSocialAsync(clienteDto.CodObraSocial.Value);

                if (os == null)
                {
                    throw new ArgumentException("No existe la obra social");
                }
            }

            Cliente? cliente = await Mapeo(clienteDto, id);

            if (cliente == null)
            {
                throw new ArgumentException("No se encontro el cliente");
            }

            return await repository.UpdateAsync(cliente);
        }

        public async Task<bool> BorrarClienteAsync(int id)
        {
            Cliente? c = await repository.GetByIdAsync(id);

            if (c == null)
            {
                throw new ArgumentException("No se encontro el cliente");
            }

            return await repository.DeleteAsync(c.IdCliente);
        }

        public async Task<bool> CrearClienteAsync(CreateClienteDto clienteDto)
        {
            //No existe el barrio

            Barrio? barrio = await RecuperarBarrioAsync(clienteDto.IdBarrio);

            if (barrio == null)
            {
                throw new ArgumentException("No existe el barrio");
            }

            //No existe la obra social

            if (clienteDto.CodObraSocial.HasValue)
            {
                ObrasSociale? os = await RecuperarObraSocialAsync(clienteDto.CodObraSocial.Value);

                if (os == null)
                {
                    throw new ArgumentException("No existe la obra social");
                }
            }
            
            Cliente c = Mapeo(clienteDto);
            return await repository.AddAsync(c);
        }

        public async Task<ReadClienteDto?> RecuperarClientePorIdAsync(int id)
        {
            Cliente? c = await repository.GetByIdAsync(id);

            if (c == null)
            {
                return null;
            }

            ReadClienteDto clienteDto = Mapeo(c);
            return clienteDto;
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

        public ReadClienteDto Mapeo(Cliente cliente)
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

            return clienteDto;
        }

        public Cliente Mapeo(CreateClienteDto clientDto)
        {
            Cliente cliente = new Cliente()
            {
                Nombre = clientDto.Nombre,
                Apellido = clientDto.Apellido,
                Documento = clientDto.Documento,
                Calle = clientDto.Calle,
                Altura = clientDto.Altura,
                IdBarrio = clientDto.IdBarrio,
                NroTelefono = clientDto.NroTelefono,
                EMail = clientDto.Email,
                CodObraSocial = clientDto.CodObraSocial
            };

            return cliente;
        }

        public async Task<Cliente?> Mapeo(UpdateClienteDto clientDto, int idCliente)
        {
            Cliente c = await repository.GetByIdAsync(idCliente);

            if (c == null)
            {
                return c;
            }

            c.Nombre = clientDto.Nombre != null ? clientDto.Nombre : c.Nombre;
            c.Apellido = clientDto.Apellido != null ? clientDto.Apellido : c.Apellido;
            c.Documento = clientDto.Documento.HasValue ? clientDto.Documento.Value : c.Documento;
            c.Calle = clientDto.Calle != null ? clientDto.Calle : c.Calle;
            c.Altura = clientDto.Altura.HasValue ? clientDto.Altura.Value : c.Altura;
            c.IdBarrio = clientDto.IdBarrio.HasValue ? clientDto.IdBarrio.Value : c.IdBarrio;
            c.NroTelefono = clientDto.NroTelefono.HasValue ? clientDto.NroTelefono.Value : c.NroTelefono;
            c.EMail = clientDto.Email != null ? clientDto.Email : c.EMail;
            c.CodObraSocial = clientDto.CodObraSocial.HasValue ? clientDto.CodObraSocial.Value : c.CodObraSocial;

            return c;
        }

        public async Task<Barrio>? RecuperarBarrioAsync(int idBarrio)
        {
            return await repository.RecuperarBarrioAsync(idBarrio);
        }

        public async Task<ObrasSociale>? RecuperarObraSocialAsync(int idObraSocial)
        {
            return await repository.RecuperarObraSocialAsync(idObraSocial);
        }
    }
}
