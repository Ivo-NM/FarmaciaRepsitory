using System.Text.Json.Serialization;
using Farmacia_Api.Dtos.FacturasDto;

namespace Farmacia_Api.Dtos.MedicamentoDto
{
    public class ReadMedicamentoDto
    {
        public int CodMedicamento { get; set; }
        public string? NombreMedicamento { get; set; }
        public bool PrescripcionMedica { get; set; }
        public string? Descripcion { get; set; }
        public string? IndicacionesUso { get; set; }
        public decimal PreUnitario { get; set; }

        [JsonIgnore]
        public ICollection<ReadDetalleFacturasDto> DetalleFacturasDto { get; set; } = new List<ReadDetalleFacturasDto>();
    }
}
