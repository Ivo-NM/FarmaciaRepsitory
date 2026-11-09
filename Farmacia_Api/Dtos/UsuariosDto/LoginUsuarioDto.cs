using System.ComponentModel.DataAnnotations;

namespace Farmacia_Api.Dtos.UsuariosDto
{
    public class LoginUsuarioDto
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasenia { get; set; }
    }
}
