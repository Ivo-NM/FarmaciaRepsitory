using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.UsuarioDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia_Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _repository;
        public AuthController(IUserRepository repo)
        {
            _repository = repo;
        }
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDto usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest(new { mesagge = "Se requiere objeto de login" });
                }
                if (!_repository.login(usuario))
                {
                    return Unauthorized(new { message = "Usuario o password incorrecto" });
                }
                var claims = new Claim[]
                {
                    new Claim("usuario", usuario.Email )
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JwtSettings:key"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken
                    (
                    issuer: "Farmacia_api",
                    audience: "Farmacia_Frontend",
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: creds
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok( tokenString );

            }
            catch (Exception ex)
            {
                // Devuelve solo el mensaje de texto, no todo el objeto ex
                return BadRequest(new { message = "Ocurrió un error al iniciar sesión", detail = ex.Message });
            }
        }
    }
}
