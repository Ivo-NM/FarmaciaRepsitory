using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos.EntregasDto
{
    public class CreateDetalleEntregasDto
    {
        public int? CodMedicamento { get; set; }
        public int? CodProducto { get; set; }
        public int Cant { get; set; }

        [JsonIgnore]
        public CreateEntregaDto? EntregaDto { get; set; }
    }
}
