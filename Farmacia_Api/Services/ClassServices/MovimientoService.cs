using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.OtrosProductosDto;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class MovimientoService: IMovimientoService
    {
        private readonly IMovimientoRepository repository;

        public MovimientoService(IMovimientoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<ReadMovimientoDto?>> RecuperarProductosVendidosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var facturas = await repository.GetVendidosByFecha(fechaInicio, fechaFin);
            var lstDto = Mapeo(facturas);
            
            var agrupados = lstDto
             .GroupBy(x => x.Producto)
             .Select(g => new ReadMovimientoDto
             {
                 Producto = g.Key,
                 Cantidad = g.Sum(x => x.Cantidad),
                 Precio = g.First().Precio
             }).ToList();

            return agrupados;
        }

        public async Task<List<ReadMovimientoDto?>> RecuperarProductosRecibidosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var entregas = await repository.GetRecibidosByFecha(fechaInicio, fechaFin);
            var lstDto = Mapeo(entregas);
            var agrupados = lstDto
                .GroupBy(x => x.Producto)
                .Select(g => new ReadMovimientoDto
                {
                    Producto = g.Key,
                    Cantidad = g.Sum(x => x.Cantidad),
                    Precio = g.First().Precio
                }).ToList();

            return agrupados;
        }

        /// <summary>
        // Metodos de mapeo
        /// </summary>
        public List<ReadMovimientoDto> Mapeo(List<Factura> facturas)
        {

            List<ReadMovimientoDto> lstDto = new List<ReadMovimientoDto>();
            
            foreach (Factura f in facturas)
            {
                foreach (DetalleFactura df in f.DetalleFacturas)
                {
                    ReadMovimientoDto movimientoDto = new ReadMovimientoDto()
                    {
                        Producto = df.CodProductoNavigation != null ? df.CodProductoNavigation.Detalle : df.CodMedicamentoNavigation.NombreMedicamento, 
                        Precio = df.PreUnitario,
                        Cantidad = df.Cantidad
                    };
                    
                    lstDto.Add(movimientoDto);
                }   
            }
            
            return lstDto;
        }

        public List<ReadMovimientoDto> Mapeo(List<Entrega> entrega)
        {
            List<ReadMovimientoDto> lstDto = new List<ReadMovimientoDto>();

            foreach (Entrega f in entrega)
            {
                foreach (DetalleEntrega df in f.DetalleEntregas)
                {
                    ReadMovimientoDto movimientoDto = new ReadMovimientoDto()
                    {
                        Producto = df.CodProductoNavigation != null ? df.CodProductoNavigation.Detalle : df.CodMedicamentoNavigation.NombreMedicamento,
                        Cantidad = df.Cant
                    };

                    lstDto.Add(movimientoDto);
                }
            }

            return lstDto;
        }

        
    }
}
