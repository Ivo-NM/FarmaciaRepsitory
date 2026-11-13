using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService service;

        public EmpleadosController(IEmpleadoService service)
        {
            this.service = service;
        }

        [HttpGet("empleados/{idSede}")]
        public async Task<IActionResult> ObtenerEmpleadosDeUnaSede(int idSede)
        {
            try
            {
                List<Empleado> lst = await service.ObtenerPorSedeAsync(idSede);
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
