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
        private readonly IMedicamentoService _medicamento;
        private readonly IProductoService _producto;
        private readonly ISucursalService _sucursal;
        private readonly IEmpleadoService _empleado;
        private readonly IInventarioService _inventario;
        public EntregaService(IEntregaRepository repository, IMedicamentoService _medicamento, IProductoService _producto, ISucursalService _sucursal, IEmpleadoService _empleado, IInventarioService inventario)
        {
            this.repository = repository;
            this._medicamento = _medicamento;
            this._producto = _producto;
            this._sucursal = _sucursal;
            this._empleado = _empleado;
            _inventario = inventario;
        }

        public async Task<bool> CrearEntregaAsync(CreateEntregaDto entrega)
        {
            //La sucursal debe existir

            Sucursale? s = await _sucursal.ObtenerSucursalPorId(entrega.NroSucursal);

            if (s == null)
            {
                throw new ArgumentException("La sucursal debe existir");
            }

            //El empleado debe existir

            Empleado? empleado = await _empleado.ObtenerEmpleadoPorIdySedeAsync(entrega.IdEmpleado,entrega.NroSucursal);

            if (empleado  == null)
            {
                throw new ArgumentException("El empleado debe existir y ser empleado de esa sucursal");
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
                    Medicamento? m = await _medicamento.ObtenerMedicamentoPorIdAsync(dto.CodMedicamento.Value);

                    if (m  == null)
                    {
                        throw new ArgumentException("El medicamento no existe");
                    }
                }

                //El producto no existe

                if (dto.CodProducto.HasValue)
                {
                    OtrosProducto? p = await _producto.ObtenerProductoPorId(dto.CodProducto.Value);

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

            foreach (var detalle in entrega.DetalleEntregasDtos)
            {
                int sucursalId = entrega.NroSucursal;
                bool esMed = detalle.CodMedicamento.HasValue;
                int itemId = esMed ? detalle.CodMedicamento.Value : detalle.CodProducto.Value;
                int cantidad = detalle.Cant;

                bool stockRestado = await _inventario.SumarStockPorEntregaAsync(sucursalId, itemId, cantidad, esMed);
                if (!stockRestado)
                {
                    throw new InvalidOperationException($"No se pudo descontar el stock para el ítem con ID {itemId}.");
                }
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

            Sucursale? s = await _sucursal.ObtenerSucursalPorId(id);

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
                        NombreMedicamento = de.CodMedicamentoNavigation != null ? de.CodMedicamentoNavigation.NombreMedicamento : null,
                        CodProducto = de.CodProducto,
                        NombreProducto = de.CodProductoNavigation != null ? de.CodProductoNavigation.Detalle : null,
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
                    NombreMedicamento = de.CodMedicamentoNavigation != null ? de.CodMedicamentoNavigation.NombreMedicamento : null,
                    CodProducto = de.CodProducto,
                    NombreProducto = de.CodProductoNavigation != null ? de.CodProductoNavigation.Detalle : null,
                    Cantidad = de.Cant
                }).ToList()
            };

            return entregaDto;
        }
    }
}
