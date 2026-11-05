using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos.EntregasDto
{
    public class CreateDetalleEntregasDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El código del medicamento debe ser un número entero positivo")]
        public int? CodMedicamento { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El código del producto debe ser un número entero positivo")]
        public int? CodProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un número entero positivo")]
        public int Cant { get; set; }

        [JsonIgnore]
        public CreateEntregaDto? EntregaDto { get; set; }
    }
}
