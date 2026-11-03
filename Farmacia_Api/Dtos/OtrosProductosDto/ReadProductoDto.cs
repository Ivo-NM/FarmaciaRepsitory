using Farmacia_Api.Dtos.FacturasDto;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos.OtrosProductosDto
{
    public class ReadProductoDto
    {
        public string Detalle { get; set; }
        public decimal Precio { get; set; }

        [JsonIgnore]
        public ReadDetalleFacturasDto? DetalleFacturasDto { get; set; }
    }
}
