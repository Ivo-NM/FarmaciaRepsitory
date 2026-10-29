using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos
{
    public class ReadSucursalDto
    {
        public int NroSucursal { get; set; }
        public string NomSede { get; set; }
        public string? Calle { get; set; }
        public int? Altura { get; set; }
        public int IdBarrio { get; set; }

        [JsonIgnore]
        public ICollection<ReadFacturaDto> FacturaDtos { get; set; } = new List<ReadFacturaDto>();
    }
}
