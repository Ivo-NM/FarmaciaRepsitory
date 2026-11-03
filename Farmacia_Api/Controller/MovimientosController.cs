using Farmacia_Api.Dtos.OtrosProductosDto;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoService service;

        public MovimientosController(IMovimientoService service)
        {
            this.service = service;
        }

        [HttpGet("productos/vendidos/{fechaInicio}/{fechaFin}")]

        public async Task<IActionResult> ObtenerProductosVendidosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReadMovimientoDto>? productoDtos = await service.RecuperarProductosVendidosPorFecha(fechaInicio, fechaFin);
                return Ok(productoDtos);
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
                    message = $"Hubo un error en el servidor: {ex.Message}"
                });
            }
        }
        [HttpGet("productos/recibidos/{fechaInicio}/{fechaFin}")]

        public async Task<IActionResult> ObtenerProductosRecibidosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReadMovimientoDto>? productoDtos = await service.RecuperarProductosRecibidosPorFecha(fechaInicio, fechaFin);
                return Ok(productoDtos);
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
                    message = $"Hubo un error en el servidor: {ex.Message}"
                });
            }
        }
    }
}
