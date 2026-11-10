using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Obras_SocialesController : ControllerBase
    {
        private readonly IUtilsService _Service;
        public Obras_SocialesController(IUtilsService service)
        {
            _Service = service;
        }
        [HttpGet("ObrasSociales")]
        public async Task<IActionResult> get()
        {
            try
            {
                var oSocial = await _Service.TraerObraSocial();
                return Ok(oSocial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObrasSociales/{id}")]
        public async Task<IActionResult> getby(int id)
        {
            try
            {
                var oSocial = await _Service.TraerObraSocialPorId(id);
                return Ok(oSocial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
