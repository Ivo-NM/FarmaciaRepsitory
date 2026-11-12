using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService service;

        public ProductosController(IProductoService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> ObtenerProductos()
        {
            try
            {
                List<OtrosProducto> lst = await service.ObtenerProductos();
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
