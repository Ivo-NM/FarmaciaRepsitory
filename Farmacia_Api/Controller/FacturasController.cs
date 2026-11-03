using Farmacia_Api.Dtos.FacturasDto;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaService _serviceFactura;
        public FacturasController(IFacturaService serviceFactura)
        {
            _serviceFactura = serviceFactura;
        }
        [HttpGet("/facturas")]
        public async Task<IActionResult> RecuperarFacturas()
        {
            try
            {
                var lista = await _serviceFactura.ObtenerFacturasAsync();
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
                var facturas = await _serviceFactura.ObtenerFacturasPorSucursalAsync(idSucursal);
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
                var factura = await _serviceFactura.ObtenerFacturasPorIdAsync(idFactura);
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

        [HttpGet("/facturas/meses/{fechaInicio}/{fechaFin}")]
        
        public async Task<IActionResult> RecuperarFacturasPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var lstFactura = await _serviceFactura.ObtenerFacturasPorFecha(fechaInicio, fechaFin);

                if (lstFactura == null)
                {
                    return NotFound(new
                    {
                        message = "No se encontraron facturas durante ese mes"
                    });
                }
                return Ok(lstFactura);
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
                bool exito = await _serviceFactura.CrearFacturaAsync(dto);

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