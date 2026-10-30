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
        private readonly IFacturaService service;

        public FarmaciaController(IFacturaService service)
        {
            this.service = service;
        }

        [HttpGet("/facturas")]
        public async Task<IActionResult> RecuperarFacturas()
        {
            try
            {
                var lista = await service.ObtenerFacturasAsync();
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
    }
}
