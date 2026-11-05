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
            //La sucursal debe existir

            Sucursale? s = await GetSucursalesByIdAsync(entrega.NroSucursal);

            if (s == null)
            {
                throw new ArgumentException("La sucursal debe existir");
            }

            //El empleado debe existir

            Empleado? empleado = await GetEmpleadoByIdAsync(entrega.IdEmpleado);

            if (empleado  == null)
            {
                throw new ArgumentException("El empleado debe existir");
            }

            foreach (CreateDetalleEntregasDto dto in entrega.DetalleEntregasDtos)
            {
                //Debe colocar un medicamento o un producto
                
                if ((!dto.CodMedicamento.HasValue && !dto.CodProducto.HasValue) ||
                    dto.CodMedicamento.HasValue && dto.CodProducto.HasValue)
                {
                    throw new ArgumentException("Debe colocar un medicamento o producto");
                }

                //El medicamento no existe

                if (dto.CodMedicamento.HasValue)
                {
                    Medicamento? m = await GetMedicamentoByIdAsync(dto.CodMedicamento.Value);

                    if (m  == null)
                    {
                        throw new ArgumentException("El medicamento no existe");
                    }
                }

                //El producto no existe

                if (dto.CodProducto.HasValue)
                {
                    OtrosProducto? p = await GetProductoByIdAsync(dto.CodProducto.Value);

                    if (p == null)
                    {
                        throw new ArgumentException("El producto no existe");
                    }
                }
            }

            //La fecha debe ser igual o anterior a la fecha de hoy

            if (entrega.Fecha > DateTime.Today)
            {
                throw new ArgumentException("La fecha debe ser igual o anterior a la fecha de hoy");
            }
            
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

        public async Task<Empleado>? GetEmpleadoByIdAsync(int idEmpleado)
        {
            return await repository.GetEmpleadoByIdAsync(idEmpleado);
        }

        public async Task<Medicamento>? GetMedicamentoByIdAsync(int codMedicamento)
        {
            return await repository.GetMedicamentoByIdAsync(codMedicamento);
        }

        public async Task<OtrosProducto>? GetProductoByIdAsync(int codProducto)
        {
            return await repository.GetProductoByIdAsync(codProducto);
        }

        public async Task<Sucursale>? GetSucursalesByIdAsync(int idSucursal)
        {
            return await repository.GetSucursalesByIdAsync(idSucursal);
        }

        public async Task<ReadEntregaDto?> RecuperarEntregaPorIdAsync(int id)
        {
            Entrega? entrega = await repository.GetByIdAsync(id);
            
            if (entrega == null)
            {
                throw new ArgumentException("No se encontro la entrega");
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
            //La sucursal debe existir

            Sucursale? s = await GetSucursalesByIdAsync(id);

            if (s  == null)
            {
                throw new ArgumentException("La sucursal debe existir");
            }
            
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
