using Farmacia_Api.Dtos.InventariosDto;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;
        public InventariosController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }


        //Obtener todos los inventarios y sus detalles
        [HttpGet("Inventarios")]
        public async Task<IActionResult> GetInv()
        {
            try
            {
                return Ok(await _inventarioService.ObtenerInventariosAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //Obtener inventario por id y sus detalles
        [HttpGet("Inventarios/{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                return Ok(await _inventarioService.ObtenerInventarioPorIdAsync(id));
            }
            catch (ArgumentException aex)
            {
                return BadRequest($"{aex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Obtener los inventarios de una sucursal y sus detalles
        [HttpGet("Inventarios/sucursal/{idSucursal}")]
        public async Task<IActionResult> GetBySucursal(int idSucursal)
        {
            try
            {
                return Ok(await _inventarioService.ObtenerInventarioPorSucursalAsync(idSucursal));
            }
            catch (ArgumentException aex)
            {
                return BadRequest(new { message = aex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"ERROR: {ex.Message}" });
            }
        }

        [HttpPost("restar-stock")]
        public async Task<IActionResult> RestarStock([FromBody] ChangeInventarioDto request)
        {
            try
            {
                var resultado = await _inventarioService.RestarStockPorVentaAsync(
                    request.SucursalId,
                    request.InsumoId,
                    request.Cantidad,
                    request.esMedicamento
                );

                if (resultado)
                {
                    return Ok(new { mensaje = "Stock descontado exitosamente." });
                }

                return BadRequest(new { mensaje = "No se pudo completar la operación de descuento de stock." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error interno en el servidor.", detalle = ex.Message });
            }
        }

        [HttpPost("Suma-stock")]
        public async Task<IActionResult> SumarStock([FromBody] ChangeInventarioDto request)
        {
            try
            {
                var resultado = await _inventarioService.SumarStockPorEntregaAsync(
                    request.SucursalId,
                    request.InsumoId,
                    request.Cantidad,
                    request.esMedicamento
                );

                if (resultado)
                {
                    return Ok(new { mensaje = "cantidad agregada al stock exitosamente." });
                }

                return BadRequest(new { mensaje = "No se pudo completar la operación de agreagacion al stock." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error interno en el servidor.", detalle = ex.Message });
            }
        }


    }
}
