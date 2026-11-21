using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormasDePagoController : ControllerBase
    {
        private readonly IUtilsService service;

        public FormasDePagoController(IUtilsService service)
        {
            this.service = service;
        }

        [HttpGet("formaspago")]
        public async Task<IActionResult> ObtenerFormasDePago()
        {
            try
            {
                List<FormasDePago> lst = await service.TraerFormasPagoAsync();
                return Ok(lst);
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
