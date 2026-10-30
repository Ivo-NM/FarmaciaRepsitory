using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos
{
    public class CreateDetalleFacturasDto
    {
        public int? CodMedicamento { get; set; }
        public int? CodProducto { get; set; }
        public decimal PreUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal? DescuentoPorObraSocial { get; set; }
        public bool Reembolsado { get; set; }

        [JsonIgnore]
        public CreateFacturaDto? FacturaDto { get; set; }
    }
}
