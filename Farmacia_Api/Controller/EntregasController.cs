using Farmacia_Api.Dtos.EntregasDto;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregasController : ControllerBase
    {
        private readonly IEntregaService _serviceEntrega;

        public EntregasController(IFacturaService serviceFactura, IEntregaService serviceEntrega)
        {
            _serviceEntrega = serviceEntrega;
        }
        [HttpGet("/entregas")]
        public async Task<IActionResult> RecuperarEntregas()
        {
            try
            {
                var lista = await _serviceEntrega.RecuperarEntregasAsync();
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
                var lista = await _serviceEntrega.RecuperarEntregasPorSedeAsync(idSede);
                return Ok(lista);
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

        [HttpGet("/entregas/{idEntrega}")]
        public async Task<IActionResult> RecuperarEntregaPorId(int idEntrega)
        {
            try
            {
                var entrega = await _serviceEntrega.RecuperarEntregaPorIdAsync(idEntrega);
                return Ok(entrega);
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

        [HttpPost("/entregas/crear")]
        public async Task<IActionResult> CrearEntrega([FromBody] CreateEntregaDto dto)
        {
            try
            {
                bool exito = await _serviceEntrega.CrearEntregaAsync(dto);

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
                    message = ex.Message
                });
            }
        }

    }
}
