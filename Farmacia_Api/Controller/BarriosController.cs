using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarriosController : ControllerBase
    {
        private readonly IUtilsService _Service;
        public BarriosController(IUtilsService service)
        {
            _Service = service;
        }
        [HttpGet("Barrios")]
        public async Task<IActionResult> get()
        {
            try
            {
                var barrios = await _Service.TraerBarrios();
                return Ok(barrios);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Barrios/{id}")]
        public async Task<IActionResult> getby(int id)
        {
            try
            {
                var barrios = await _Service.TraerBarrioPorId(id);
                return Ok(barrios);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
               return BadRequest(ex.Message);
            }
        }

    }
}
