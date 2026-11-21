using System.ComponentModel.DataAnnotations;

namespace Farmacia_Api.Dtos.FacturasDto
{
    public class CreateFacturaDto
    {
        [Required(ErrorMessage = "La sucursal es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de sucursal debe ser un número entero positivo")]
        public int NroSucursal { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El empleado es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El id del empleado debe ser un número entero positivo")]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El cliente es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El id del cliente debe ser un número entero positivo")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "La forma de pago es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "El id de la forma de pago debe ser un número entero positivo")]
        public int IdFormaPago { get; set; }
        public virtual ICollection<CreateDetalleFacturasDto> DetalleFacturasDtos { get; set; } = new List<CreateDetalleFacturasDto>();
    }
}
