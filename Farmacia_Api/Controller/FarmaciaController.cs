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
        private readonly IFacturaService serviceFactura;
        private readonly IEntregaService serviceEntrega;

        public FarmaciaController(IFacturaService serviceFactura, IEntregaService serviceEntrega)
        {
            this.serviceFactura = serviceFactura;
            this.serviceEntrega = serviceEntrega;
        }

        [HttpGet("/facturas")]
        public async Task<IActionResult> RecuperarFacturas()
        {
            try
            {
                var lista = await serviceFactura.ObtenerFacturasAsync();
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
                var facturas = await serviceFactura.ObtenerFacturasPorSucursalAsync(idSucursal);
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
                var factura = await serviceFactura.ObtenerFacturasPorIdAsync(idFactura);
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
                bool exito = await serviceFactura.CrearFacturaAsync(dto);

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

        [HttpGet("/entregas")]
        public async Task<IActionResult> RecuperarEntregas()
        {
            try
            {
                var lista = await serviceEntrega.RecuperarEntregasAsync();
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

        [HttpGet("/entregas/sedes/{idSede}")]
        public async Task<IActionResult> RecuperarEntregasPorSede(int idSede)
        {
            try
            {
                var lista = await serviceEntrega.RecuperarEntregasPorSedeAsync(idSede);
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

        [HttpGet("/entregas/{idEntrega}")]
        public async Task<IActionResult> RecuperarEntregaPorId(int idEntrega)
        {
            try
            {
                var entrega = await serviceEntrega.RecuperarEntregaPorIdAsync(idEntrega);
                return Ok(entrega);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"ERROR: {ex.Message}"
                });
            }
        }

        [HttpPost("/entregas/crear")]
        public async Task<IActionResult> CrearEntrega([FromBody] CreateEntregaDto dto)
        {
            try
            {
                bool exito = await serviceEntrega.CrearEntregaAsync(dto);

                if (!exito)
                {
                    return BadRequest(new
                    {
                        mensaje = "No se pudo crear la entrega"
                    });
                }

                return Ok(new
                {
                    mensaje = "Se pudo crear la entrega"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }
    }
}
