using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos.FacturasDto
{
    public class CreateDetalleFacturasDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El código del medicamento debe ser un número entero positivo")]
        public int? CodMedicamento { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El código del producto debe ser un número entero positivo")]
        public int? CodProducto { get; set; }

        [Required(ErrorMessage = "Se requiere el precio unitario")]
        public decimal PreUnitario { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un número entero positivo")]
        public int Cantidad { get; set; }
        public decimal? DescuentoPorObraSocial { get; set; }

        [Required(ErrorMessage = "El reembolso es requerido")]
        public bool Reembolsado { get; set; }

        [JsonIgnore]
        public CreateFacturaDto? FacturaDto { get; set; }
    }
}
