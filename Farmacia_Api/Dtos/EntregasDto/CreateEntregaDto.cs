using System.ComponentModel.DataAnnotations;

namespace Farmacia_Api.Dtos.EntregasDto
{
    public class CreateEntregaDto
    {
        [Required(ErrorMessage = "El número de sucursal es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de sucursal debe ser un número entero positivo")]
        public int NroSucursal { get; set; }
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "La firma de la empresa es requerida")]
        [MaxLength(30, ErrorMessage = "La firma de la empresa debe tener máximo 30 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "La firma de la empresa solo puede contener letras y espacios.")]
        public string FirmaEmpresa { get; set; }

        [Required(ErrorMessage = "La respuesta de la entrega es requerida")]
        [MaxLength(20, ErrorMessage = "La firma de la empresa debe tener máximo 30 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "La respuesta de la entrega solo puede contener letras y espacios.")]
        public string RespuestaEntrega { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El id del empleado debe ser un número entero positivo")]
        [Required(ErrorMessage = "El id del empleado es requerido")]
        public int IdEmpleado { get; set; }

        public IEnumerable<CreateDetalleEntregasDto> DetalleEntregasDtos { get; set; } = new List<CreateDetalleEntregasDto>();
    }
}
