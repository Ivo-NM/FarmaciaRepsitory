using System.ComponentModel.DataAnnotations;

namespace Farmacia_Api.Dtos.ClientesDto
{
    public class CreateClienteDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(20, ErrorMessage = "El nombre debe tener como máximo 20 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(20, ErrorMessage = "El apellido debe tener como máximo 20 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El documento es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El documento debe ser un número entero positivo")]
        public int Documento { get; set; }

        [StringLength(30, ErrorMessage = "La calle debe tener como máximo 30 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "La calle solo puede contener letras y espacios.")]
        public string? Calle { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El altura debe ser un número entero positivo")]
        public int? Altura { get; set; }

        [Required(ErrorMessage = "El barrio es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El id del barrio debe ser un número entero positivo")]
        public int IdBarrio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de teléfono debe ser un número entero positivo")]
        public int? NroTelefono { get; set; }

        [StringLength(30, ErrorMessage = "El email debe tener como máximo 30 caracteres")]
        [EmailAddress(ErrorMessage = "El correo electrónico ingresado no tiene un formato válido")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El email solo puede contener letras y espacios.")]
        public string? Email { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El id de la obra social debe ser un número entero positivo")]
        public int? CodObraSocial { get; set; }
    }
}