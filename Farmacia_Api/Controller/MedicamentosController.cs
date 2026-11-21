using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly IMedicamentoService service;

        public MedicamentosController(IMedicamentoService service)
        {
            this.service = service;
        }

        [HttpGet("medicamentos")]
        public async Task<IActionResult> ObtenerMedicamentos()
        {
            try
            {
                List<Medicamento> lst = await service.ObtenerMedicamentosAsync();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Error en el servidor: {ex.Message}"
                });
            }
        }
    }
}
