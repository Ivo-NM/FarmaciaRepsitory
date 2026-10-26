using Farmacia_Api.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmaciaController : ControllerBase
    {
        private readonly IClienteRepository _repo;
        public FarmaciaController(IClienteRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok( await _repo.ObtenerTodosAsync());
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }





    }
}
