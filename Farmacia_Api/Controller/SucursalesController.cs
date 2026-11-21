using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalService service;

        public SucursalesController(ISucursalService service)
        {
            this.service = service;
        }

        [HttpGet("sucursales")]
        public async Task<IActionResult> ObtenerSucursales()
        {
            try
            {
                List<Sucursale> lst = await service.ObtenerSucursales();
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
