using Farmacia_Api.Models;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos
{
    public class ReadProductoDto
    {
        public int CodProducto { get; set; }
        public string Detalle { get; set; }
        public decimal PreUnitario { get; set; }

        [JsonIgnore]
        public ICollection<ReadDetalleFacturasDto> DetalleFacturas { get; set; } = new List<ReadDetalleFacturasDto>();
    }
}