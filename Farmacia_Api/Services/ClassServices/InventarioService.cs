using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.InventariosDto;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository _inventarioRepository;
        public InventarioService(IInventarioRepository inventarioRepository)
        {
            _inventarioRepository = inventarioRepository;
        }

        public Task<bool> CrearInventarioAsync(CreateInventarioDto inventario)
        {
            throw new NotImplementedException();
        }
        public async Task<ReadInventarioDto?> ObtenerInventarioPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El id debe ser mayor a 0");
            }
            
            var inventario = await _inventarioRepository.GetByIdAsync(id);
            if (inventario == null)
            {
                throw new ArgumentException("No existe este inventario");
            }

            var readDto = Mapeo(inventario);
            return readDto;
        }

        public async Task<IEnumerable<ReadInventarioDto>> ObtenerInventarioPorSucursalAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El id debe ser mayor a 0");
            }

            var inventarios = (await _inventarioRepository.GetByInSedeAsync(id)).ToList();

            if (inventarios == null)
            {
                throw new ArgumentException("No existe este inventario");
            }

            var listadto = Mapeo(inventarios);

            return listadto;
        }
        public async Task<IEnumerable<ReadInventarioDto>> ObtenerInventariosAsync()
        {
            var Inventario = (await _inventarioRepository.GetAllAsync()).ToList();
            var listadto = Mapeo(Inventario);
            return listadto;
        }

        /// <summary>
        // Metodos y validaciones para restar o agregar insumos al inventario
        /// </summary>
        
        public async Task<bool> RestarStockPorVentaAsync(int sucursalId, int insumoId, int cantidadVendida, bool esMedicamento)
        {
            if (cantidadVendida <= 0) throw new ArgumentException("La cantidad debe ser mayor a 0.");

            // Buscamos el detalle
            var detalleInventario = await _inventarioRepository.VerificarStockDisponibleAsync(sucursalId, insumoId, cantidadVendida, esMedicamento);

            if (detalleInventario == null)
            {
                throw new InvalidOperationException("El insumo no se encuentra registrado en el inventario de la sucursal.");
            }

            if (detalleInventario.Stock < cantidadVendida)
            {
                throw new InvalidOperationException($"Stock insuficiente (Disponible: {detalleInventario.Stock}, Solicitado: {cantidadVendida}).");
            }

            // Modificamos y guardamos usando el repositorio optimizado
            return await _inventarioRepository.ModificarStockAsync(detalleInventario, cantidadVendida, true);
        }
        public async Task<bool> SumarStockPorEntregaAsync(int sucursalId, int InsumoId, int cantidadRecibida, bool esMedicamento)
        {
            if (cantidadRecibida <= 0) throw new ArgumentException("La cantidad debe ser mayor a 0.");

            // Buscamos el detalle
            var detalleInventario = await _inventarioRepository.VerificarStockDisponibleAsync(sucursalId, InsumoId, cantidadRecibida, esMedicamento);

            if (detalleInventario == null)
            {
                throw new InvalidOperationException("El insumo no se encuentra registrado en el inventario de la sucursal.");
            }
            // Modificamos y guardamos usando el repositorio optimizado
            return await _inventarioRepository.ModificarStockAsync(detalleInventario, cantidadRecibida, false);

        }

        /// <summary>
        // Metodos internos Mapeo()
        /// </summary>
        private List<ReadInventarioDto> Mapeo(List<Inventario> Inventarios)
        {
            List<ReadInventarioDto> listaInv = new List<ReadInventarioDto>();

            foreach (Inventario inv in Inventarios)
            {
                ReadInventarioDto Dto = new ReadInventarioDto()
                {
                    NroInventario = inv.NroInventario,
                    NomSede = inv.NroSucursalNavigation.NomSede,
                    FechaInventario = inv.FechaInventario,
                    IdEmpleado = inv.IdEmpleado,
                    //NomEmpleado = inv.IdEmpleadoNavigation.NomEmpleado,
                    DetalleInventarios = inv.DetalleInventarios.Select(di => new ReadDetalleInventarioDto()
                    {
                        CodMedicamento = di.CodMedicamento,
                        NomMedicamento = di.CodMedicamentoNavigation != null ? di.CodMedicamentoNavigation.NombreMedicamento : null,
                        CodProducto = di.CodProducto,
                        NomProducto = di.CodProductoNavigation != null ? di.CodProductoNavigation.Detalle : null,
                        Stock = di.Stock
                    }).ToList(),
                };
                listaInv.Add(Dto);
            }
            return listaInv;
        }
        private ReadInventarioDto Mapeo(Inventario Inventario)
        {
            ReadInventarioDto Dto = new ReadInventarioDto()
            {
                NroInventario = Inventario.NroInventario,
                NomSede = Inventario.NroSucursalNavigation.NomSede,
                FechaInventario = Inventario.FechaInventario,
                IdEmpleado = Inventario.IdEmpleado,
                //NomEmpleado = inv.IdEmpleadoNavigation.NomEmpleado,
                DetalleInventarios = Inventario.DetalleInventarios.Select(di => new ReadDetalleInventarioDto()
                {
                    CodMedicamento = di.CodMedicamento,
                    NomMedicamento = di.CodMedicamentoNavigation != null ? di.CodMedicamentoNavigation.NombreMedicamento : null,
                    CodProducto = di.CodProducto,
                    NomProducto = di.CodProductoNavigation != null ? di.CodProductoNavigation.Detalle : null,
                    Stock = di.Stock
                }).ToList()
            };
            return Dto;
        }
    }
}
