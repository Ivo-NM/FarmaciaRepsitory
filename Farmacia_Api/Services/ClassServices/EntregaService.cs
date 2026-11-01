using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.EntregasDto;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using System.Reflection.Metadata.Ecma335;

namespace Farmacia_Api.Services.ClassServices
{
    public class EntregaService : IEntregaService
    {
        private readonly IEntregaRepository repository;

        public EntregaService(IEntregaRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> CrearEntregaAsync(CreateEntregaDto entrega)
        {
            Entrega e = new Entrega()
            {
                NroSucursal = entrega.NroSucursal,
                Fecha = entrega.Fecha,
                FirmaEmpresa = entrega.FirmaEmpresa,
                RespEntrega = entrega.RespuestaEntrega,
                IdEmpleado = entrega.IdEmpleado,
            };

            e.DetalleEntregas = entrega.DetalleEntregasDtos.Select(de => new DetalleEntrega()
            {
                CodMedicamento = de.CodMedicamento,
                CodProducto = de.CodProducto,
                Cant = de.Cant
            })
            .ToList();

            return await repository.AddAsync(e);
        }

        public async Task<ReadEntregaDto?> RecuperarEntregaPorIdAsync(int id)
        {
            Entrega? entrega = await repository.GetByIdAsync(id);
            
            if (entrega == null)
            {
                return null;
            }

            ReadEntregaDto entregaDto = Mapeo(entrega);
            return entregaDto;
        }

        public async Task<IEnumerable<ReadEntregaDto>> RecuperarEntregasAsync()
        {
            List<Entrega> entregas = (await repository.GetAllAsync()).ToList();
            var lstEntregas = Mapeo(entregas);
            return lstEntregas;
        }

        public async Task<IEnumerable<ReadEntregaDto>> RecuperarEntregasPorSedeAsync(int id)
        {
            List<Entrega> entregas = (await repository.GetAllInSedeAsync(id)).ToList();
            var lstEntregas = Mapeo(entregas);
            return lstEntregas;
        }
        private List<ReadEntregaDto> Mapeo(List<Entrega> lista)
        {
            List<ReadEntregaDto> listaDto = new List<ReadEntregaDto>();

            foreach (Entrega entrega in lista)
            {
                ReadEntregaDto entregaDto = new ReadEntregaDto()
                {
                    NroEntrega = entrega.NroEntrega,
                    Fecha = entrega.Fecha,
                    RespuestaEntrega = entrega.RespEntrega,
                    IdEmpleado = entrega.IdEmpleado,
                    DetalleEntregasDtos = entrega.DetalleEntregas.Select(de => new ReadDetalleEntregasDto()
                    {
                        CodMedicamento = de.CodMedicamento,
                        NombreMedicamento = de.CodMedicamentoNavigation != null ? de.CodMedicamentoNavigation.NombreMedicamento : string.Empty,
                        CodProducto = de.CodProducto,
                        NombreProducto = de.CodProductoNavigation != null ? de.CodProductoNavigation.Detalle : string.Empty,
                        Cantidad = de.Cant
                    }).ToList()
                };

                listaDto.Add(entregaDto);
            }

            return listaDto;
        }

        private ReadEntregaDto Mapeo(Entrega entrega)
        {
            ReadEntregaDto entregaDto = new ReadEntregaDto()
            {
                NroEntrega = entrega.NroEntrega,
                Fecha = entrega.Fecha,
                RespuestaEntrega = entrega.RespEntrega,
                IdEmpleado = entrega.IdEmpleado,
                DetalleEntregasDtos = entrega.DetalleEntregas.Select(de => new ReadDetalleEntregasDto()
                {
                    CodMedicamento = de.CodMedicamento,
                    NombreMedicamento = de.CodMedicamentoNavigation != null ? de.CodMedicamentoNavigation.NombreMedicamento : string.Empty,
                    CodProducto = de.CodProducto,
                    NombreProducto = de.CodProductoNavigation != null ? de.CodProductoNavigation.Detalle : string.Empty,
                    Cantidad = de.Cant
                }).ToList()
            };

            return entregaDto;
        }
    }
}
