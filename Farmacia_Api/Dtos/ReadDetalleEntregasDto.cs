using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos
{
    public class ReadDetalleEntregasDto
    {
        public int? CodMedicamento { get; set; }
        public string? NombreMedicamento { get; set; }
        public int? CodProducto { get; set; }
        public string? NombreProducto { get; set; }
        public int Cantidad { get; set; }

        [JsonIgnore]
        public ReadEntregaDto? EntregaDto { get; set; }
    }
}
