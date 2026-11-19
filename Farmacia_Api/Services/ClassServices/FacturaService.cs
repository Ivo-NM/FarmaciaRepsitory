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
        private readonly IMedicamentoService _medicamento;
        private readonly IProductoService _producto;
        private readonly ISucursalService _sucursal;
        private readonly IEmpleadoService _empleado;
        public FacturaService(IFacturaRepository repository, IInventarioService _inventario, IMedicamentoService _medicamento, IProductoService _producto, ISucursalService _sucursal, IEmpleadoService _empleado)
        {
            this.repository = repository;
            this._inventario = _inventario;
            this._medicamento = _medicamento;
            this._producto = _producto;
            this._sucursal = _sucursal;
            this._empleado = _empleado;
        }

        public async Task<bool> CrearFacturaAsync(CreateFacturaDto facturaDto)
        {   
            //La sucursal debe existir

            Sucursale? sede = await _sucursal.ObtenerSucursalPorId(facturaDto.NroSucursal);

            if (sede == null)
            {
                throw new ArgumentException("La sucursal debe existir");
            }

            //La fecha debe ser posterior o igual a la fecha de hoy

            if (facturaDto.Fecha > DateTime.Now)
            {
                throw new ArgumentException("La fecha debe ser posterior o igual a la fecha de hoy");
            }

            //No existe ese empleado

            Empleado? e = await _empleado.ObtenerEmpleadoPorIdySedeAsync(facturaDto.IdEmpleado,facturaDto.NroSucursal);

            if (e == null)
            {
                throw new ArgumentException("el empleado debe existir y formar parte de esa sucursal");
            }

            //No existe ese cliente

            Cliente? c = await ObtenerClientePorIdAsync(facturaDto.IdCliente);

            if (c == null)
            {
                throw new ArgumentException("El id del cliente debe existir");
            }

            //No existe esa forma de pago

            FormasDePago? fp = await ObtenerFormaDePagoPorIdAsync(facturaDto.IdFormaPago);

            if (fp == null)
            {
                throw new ArgumentException("No existe esa forma de pago");
            }

            foreach (CreateDetalleFacturasDto detalle in facturaDto.DetalleFacturasDtos)
            {
                //Debe poner código de medicamento o código de producto

                if (!detalle.CodMedicamento.HasValue && !detalle.CodProducto.HasValue||
                    detalle.CodMedicamento.HasValue && detalle.CodProducto.HasValue)
                {
                    throw new ArgumentException("Debe poner código de medicamento o código de producto");
                }

                //El medicamento no existe

                if (detalle.CodMedicamento.HasValue)
                {
                    Medicamento? m = await _medicamento.ObtenerMedicamentoPorIdAsync(detalle.CodMedicamento.Value);

                    if (m  == null)
                    {
                        throw new ArgumentException("El medicamento no existe");
                    }
                }

                //El producto no existe

                if (detalle.CodProducto.HasValue)
                {
                    OtrosProducto? p = await _producto.ObtenerProductoPorId(detalle.CodProducto.Value);

                    if (p == null)
                    {
                        throw new ArgumentException("El producto no existe");
                    }
                }

                //Reembolsado debe ser true o false

                if (detalle.Reembolsado != false && detalle.Reembolsado != true)
                {
                    throw new ArgumentException("Reembolsado debe ser true o false");
                }
            }

            //Verificamos que haya stock del producto seleccionado
            foreach (var detalle in facturaDto.DetalleFacturasDtos)
            {
                int sucursalId = facturaDto.NroSucursal;
                bool esMed = detalle.CodMedicamento.HasValue;
                int itemId = esMed ? detalle.CodMedicamento.Value : detalle.CodProducto.Value;
                int cantidad = detalle.Cantidad;

                bool stockRestado = await _inventario.RestarStockPorVentaAsync(sucursalId, itemId, cantidad, esMed);
                if (!stockRestado)
                {
                    throw new InvalidOperationException($"No se pudo descontar el stock para el ítem con ID {itemId}.");
                }
            }


            Factura factura = new Factura()
            {
                NroSucursal = facturaDto.NroSucursal,
                Fecha = facturaDto.Fecha,
                IdEmpleado = facturaDto.IdEmpleado,
                IdCliente = facturaDto.IdCliente,
                IdFormaPago = facturaDto.IdFormaPago,

                DetalleFacturas = facturaDto.DetalleFacturasDtos.Select(df => new DetalleFactura
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

            return await repository.AddAsync(factura);
        }


        public async Task<Cliente>? ObtenerClientePorIdAsync(int id)
        {
            return await repository.GetClienteByIdAsync(id);
        }
        public async Task<IEnumerable<ReadFacturaDto>> ObtenerFacturasAsync()
        {
            var facturas = (await repository.GetAllAsync()).ToList();
            var lista = Mapeo(facturas);
            return lista;
        }
        public async Task<ReadFacturaDto?> ObtenerFacturasPorIdAsync(int id)
        {
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
                throw new ArgumentException("No se encontraron facturas");
            }

            List<ReadFacturaDto> facturasDto = Mapeo(facturas);
            return facturasDto;
        }
        public async Task<IEnumerable<ReadFacturaDto>>? ObtenerFacturasPorSucursalAsync(int id)
        {
            //La sucursal debe existir

            Sucursale? s = await _sucursal.ObtenerSucursalPorId(id);

            if (s  == null)
            {
                throw new ArgumentException("La sucursal debe existir");
            }
            
            List<Factura>? facturas = (await repository.GetAllInSedeAsync(id)).ToList();
            
            if (facturas == null)
            {
                throw new ArgumentException("No se encontraron facturas");
            }

            var lista = Mapeo(facturas);
            return lista;
        }
        public async Task<FormasDePago> ObtenerFormaDePagoPorIdAsync(int id)
        {
            return await repository.GetFormaDePagoByIdAsync(id);
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
                    NomEmpleado = itemF.IdEmpleadoNavigation.ApellEmpleado + ", " +itemF.IdEmpleadoNavigation.NomEmpleado,
                    IdFormaPago = itemF.IdFormaPago,
                    DetalleFormaPago = itemF.IdFormaPagoNavigation.Descripcion,

                    DetalleFacturasDto = itemF.DetalleFacturas.Select(x => new ReadDetalleFacturasDto()
                    {
                        nombreMed = x.CodMedicamentoNavigation != null ? x.CodMedicamentoNavigation.NombreMedicamento : null,
                        nombreProd = x.CodProductoNavigation != null ? x.CodProductoNavigation.Detalle : null,
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
                NomEmpleado = factura.IdEmpleadoNavigation.ApellEmpleado + ", " + factura.IdEmpleadoNavigation.NomEmpleado,
                IdFormaPago = factura.IdFormaPago,
                DetalleFormaPago = factura.IdFormaPagoNavigation.Descripcion,

                DetalleFacturasDto = factura.DetalleFacturas.Select(x => new ReadDetalleFacturasDto()
                {
                    nombreMed = x.CodMedicamentoNavigation != null ? x.CodMedicamentoNavigation.NombreMedicamento : null,
                    nombreProd = x.CodProductoNavigation != null ? x.CodProductoNavigation.Detalle : null,
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
