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
                var lista = await service.ObtenerFacturasAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"ERROR: {ex.Message}"
                });
            }
        }

        [HttpGet("/facturas/sucursal/{idSucursal}")]
        public async Task<IActionResult> RecuperarFacturaPorSucursal(int idSucursal)
        {
            try
            {
                var facturas = await service.ObtenerFacturasPorSucursalAsync(idSucursal);
                return Ok(facturas);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"ERROR: {ex.Message}"
                });
            }
        }

        [HttpGet("/facturas/{idFactura}")]
        public async Task<IActionResult> RecuperarFacturaPorId(int idFactura)
        {
            try
            {
                var factura = await service.ObtenerFacturasPorIdAsync(idFactura);
                return Ok(factura);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"ERROR: {ex.Message}"
                });
            }
        }

        [HttpPost("/facturas/crear")]
        public async Task<IActionResult> CrearFactura([FromBody] CreateFacturaDto dto)
        {
            try
            {
                bool exito = await service.CrearFacturaAsync(dto);

                if (!exito)
                {
                    return BadRequest(new
                    {
                        message = "Error al crear la factura"
                    });
                }

                return Ok(new
                {
                    message = "Factura creada"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
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
