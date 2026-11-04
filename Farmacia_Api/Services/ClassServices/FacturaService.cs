using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.FacturasDto;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using System.Diagnostics;

namespace Farmacia_Api.Services.ClassServices
{
    public class FacturaService: IFacturaService
    {
        private readonly IFacturaRepository repository;
        private readonly IInventarioService _inventario;

        public FacturaService(IFacturaRepository repository, IInventarioService _inventario)
        {
            this.repository = repository;
            this._inventario = _inventario;
        }

        public async Task<bool> CrearFacturaAsync(CreateFacturaDto factura)
        {
            //La sucursal es requerida

            if (factura.NroSucursal <= 0)
            {
                throw new ArgumentException("La sucursal es requerida");
            }
            
            //La sucursal debe existir

            Sucursale? sede = await ObtenerSedePorIdAsync(factura.NroSucursal);

            if (sede == null)
            {
                throw new ArgumentException("La sucursal debe existir");
            }

            //La fecha debe ser posterior a la fecha de hoy

            if (factura.Fecha < DateTime.Now)
            {
                throw new ArgumentException("La fecha debe ser posterior a la fecha de hoy");
            }

            //El id del empleado es requerido

            if (factura.IdEmpleado <= 0)
            {
                throw new ArgumentException("El id del empleado es requerido");
            }

            //No existe ese empleado

            Empleado? e = await ObtenerEmpleadoPorIdAsync(factura.IdEmpleado);

            if (e == null)
            {
                throw new ArgumentException("No existe ese empleado");
            }

            //El id del cliente es requerido

            if (factura.IdCliente <= 0)
            {
                throw new ArgumentException("El id del cliente es requerido");
            }

            //No existe ese cliente

            Cliente? c = await ObtenerClientePorIdAsync(factura.IdCliente);

            if (c == null)
            {
                throw new ArgumentException("El id del cliente debe existir");
            }

            //El id de la forma de pago es requerido

            if (factura.IdFormaPago <= 0)
            {
                throw new ArgumentException("El id de la forma de pago es requerido");
            }

            //No existe esa forma de pago

            FormasDePago? fp = await ObtenerFormaDePagoPorIdAsync(factura.IdFormaPago);

            if (fp == null)
            {
                throw new ArgumentException("No existe esa forma de pago");
            }

            foreach (CreateDetalleFacturasDto detalle in factura.DetalleFacturasDtos)
            {
                //Debe poner código de medicamento o código de producto

                if (detalle.CodMedicamento <= 0 && detalle.CodProducto <= 0)
                {
                    throw new ArgumentException("Debe poner código de medicamento o código de producto");
                }

                //El precio unitario debe ser mayor a 0

                if (detalle.PreUnitario <= 0)
                {
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }

                //La cantidad debe ser mayor a 0

                if (detalle.Cantidad <= 0)
                {
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }
            }

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

            /*

            var id = f.NroSucursal;

            var id_prod = f.DetalleFacturas.Select(f => f.CodProducto) != null ? f.DetalleFacturas.Select(f => f.CodProducto) : f.DetalleFacturas.Select(f => f.CodMedicamento);
            var esMed = true;

            if (f.DetalleFacturas.Select(f => f.CodProducto) != null)
            {
                esMed = false;
            }

            var cant = f.DetalleFacturas.Select(f => f.Cantidad);

            if (_inventario.RestarStockPorVentaAsync(id,id_prod,cant,esMed))
            {
                //Restar stock -> Resta en el inventario la cantidad que diga la factura
                //Sumar stock -> Suma en el inventario la cantidad que diga la entrega
            }

            */

            return await repository.AddAsync(f);
        }
        public async Task<Cliente>? ObtenerClientePorIdAsync(int id)
        {
            return await repository.GetClienteByIdAsync(id);
        }

        public async Task<Empleado>? ObtenerEmpleadoPorIdAsync(int id)
        {
            return await repository.GetEmpleadoByIdAsync(id);
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

        public async Task<List<ReadFacturaDto>> ObtenerFacturasPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior o igual a la fecha de fin");
            }

            List<Factura>? facturas = await repository.GetByDateAsync(fechaInicio, fechaFin);

            if (facturas == null)
            {
                return null;
            }

            List<ReadFacturaDto> facturasDto = Mapeo(facturas);
            return facturasDto;
        }

        public async Task<IEnumerable<ReadFacturaDto>> ObtenerFacturasPorSucursalAsync(int id)
        {
            //El id debe ser mayor a 0

            if (id <= 0)
            {
                throw new ArgumentException("El id debe ser mayor a 0");
            }

            //La sucursal debe existir

            Sucursale? s = await ObtenerSedePorIdAsync(id);

            if (s  == null)
            {
                throw new ArgumentException("La sucursal debe existir");
            }
            
            var facturas = (await repository.GetAllInSedeAsync(id)).ToList();
            var lista = Mapeo(facturas);
            return lista;
        }

        public async Task<FormasDePago> ObtenerFormaDePagoPorIdAsync(int id)
        {
            return await repository.GetFormaDePagoByIdAsync(id);
        }

        public async Task<Sucursale>? ObtenerSedePorIdAsync(int id)
        {
            return await repository.GetSedeByIdAsync(id);
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
