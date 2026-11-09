using Farmacia_Api.Dtos.UsuariosDto;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService service;

        public UsuariosController(IUsuarioService service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto dto)
        {
            try
            {
                bool exito = await service.LoginUsuario(dto);

                if (!exito)
                {
                    return BadRequest(new
                    {
                        message = "Password y/o correo incorrectos"
                    });
                }

                return Ok(new
                {
                    message = "Inicio de sesión exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Hubo un error en el servidor: {ex.Message}"
                });
            }
        }
    }
}
