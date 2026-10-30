using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using System.Diagnostics;

namespace Farmacia_Api.Services.ClassServices
{
    public class FacturaService: IFacturaService
    {
        private readonly IFacturaRepository repository;

        public FacturaService(IFacturaRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> CrearFacturaAsync(CreateFacturaDto factura)
        {
            Factura f = new Factura()
            {
                NroSucursal = factura.NroSucursal,
                Fecha = factura.Fecha,
                IdEmpleado = factura.IdEmpleado,
                IdCliente = factura.IdCliente,
                IdFormaPago = factura.IdFormaPago,

                DetalleFacturas = factura.DetalleFacturasDtos.Select(df => new DetalleFactura
                {
                    CodMedicamento = df.CodMedicamento,
                    CodProducto = df.CodProducto,
                    PreUnitario = df.PreUnitario,
                    Cantidad = df.Cantidad,
                    DescuentoPorObraSocial = df.DescuentoPorObraSocial,
                    Reembolsado = df.Reembolsado
                })
                .ToList()
            };

            return await repository.AddAsync(f);
        }

        public async Task<IEnumerable<ReadFacturaDto>> ObtenerFacturasAsync()
        {
            var facturas = (await repository.GetAllAsync()).ToList();
            var lista = Mapeo(facturas);
            return lista;
        }

        public async Task<ReadFacturaDto?> ObtenerFacturasPorIdAsync(int id)
        {
            //El id debe ser mayor a 0

            if (id <= 0)
            {
                throw new ArgumentException("El id debe ser mayor a 0");
            }

            var factura = await repository.GetByIdAsync(id);

            if (factura == null)
            {
                throw new ArgumentException("No existe la factura");
            }

            var facturaDto = Mapeo(factura);
            return facturaDto;
        }

        public async Task<IEnumerable<ReadFacturaDto>> ObtenerFacturasPorSucursalAsync(int id)
        {
            //El id debe ser mayor a 0

            if (id <= 0)
            {
                throw new ArgumentException("El id debe ser mayor a 0");
            }
            
            var facturas = (await repository.GetAllInSedeAsync(id)).ToList();
            var lista = Mapeo(facturas);
            return lista;
        }

        private List<ReadFacturaDto> Mapeo(List<Factura> facturas)
        {
       
            List<ReadFacturaDto> listaFacturaDto = new List<ReadFacturaDto>();

            foreach (Factura itemF in facturas)
            {
                ReadFacturaDto facturaDto = new ReadFacturaDto()
                {
                    NroFactura = itemF.NroFactura,
                    Nombsede = itemF.NroSucursalNavigation.NomSede,
                    Fecha = itemF.Fecha,
                    IdEmpleado = itemF.IdEmpleado,
                    IdFormaPago = itemF.IdFormaPago,

                    DetalleFacturasDto = itemF.DetalleFacturas.Select(x => new ReadDetalleFacturasDto()
                    {
                        nombreMed = x.CodMedicamentoNavigation != null ? x.CodMedicamentoNavigation.NombreMedicamento : string.Empty,
                        nombreProd = x.CodProductoNavigation != null ? x.CodProductoNavigation.Detalle : string.Empty,
                        PreUnitario = x.PreUnitario,
                        Cantidad = x.Cantidad,
                        DescuentoObraSocial = x.DescuentoPorObraSocial,
                        Reembolsado = x.Reembolsado
                    }).ToList()

                }; 

                listaFacturaDto.Add(facturaDto);
            }
            return listaFacturaDto;
        }

        private ReadFacturaDto Mapeo(Factura factura)
        {
            ReadFacturaDto facturaDto = new ReadFacturaDto()
            {
                NroFactura = factura.NroFactura,
                Nombsede = factura.NroSucursalNavigation.NomSede,
                Fecha = factura.Fecha,
                IdEmpleado = factura.IdEmpleado,
                IdFormaPago = factura.IdFormaPago,

                DetalleFacturasDto = factura.DetalleFacturas.Select(x => new ReadDetalleFacturasDto()
                {
                    nombreMed = x.CodMedicamentoNavigation != null ? x.CodMedicamentoNavigation.NombreMedicamento : string.Empty,
                    nombreProd = x.CodProductoNavigation != null ? x.CodProductoNavigation.Detalle : string.Empty,
                    PreUnitario = x.PreUnitario,
                    Cantidad = x.Cantidad,
                    DescuentoObraSocial = x.DescuentoPorObraSocial,
                    Reembolsado = x.Reembolsado
                }).ToList()


            };

            return facturaDto;
        }
    }
}
