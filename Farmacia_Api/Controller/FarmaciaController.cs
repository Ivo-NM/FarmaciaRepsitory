using Farmacia_Api.Dtos;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmaciaController : ControllerBase
    {
        private readonly IFacturaService service;

        public FarmaciaController(IFacturaService service)
        {
            this.service = service;
        }

        [HttpGet("/facturas")]
        public async Task<IActionResult> RecuperarFacturas()
        {
            try
            {
                List<Factura> listaFacturaCargada = (await service.ObtenerFacturasAsync()).ToList();
                List<ReadFacturaDto> listaFacturaDto = new List<ReadFacturaDto>();

                foreach (Factura factura in listaFacturaCargada)
                {
                    ReadFacturaDto facturaDto = new ReadFacturaDto()
                    {
                        NroFactura = factura.NroFactura,
                        NroSucursal = factura.NroSucursal,
                        Fecha = factura.Fecha,
                        IdEmpleado = factura.IdEmpleado,
                        IdCliente = factura.IdCliente,
                        IdFormaPago = factura.IdFormaPago,
                    };

                    listaFacturaDto.Add(facturaDto);
                }

                return Ok(listaFacturaDto);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"ERROR: {ex.Message}"
                });
            }
        }
    }
}
