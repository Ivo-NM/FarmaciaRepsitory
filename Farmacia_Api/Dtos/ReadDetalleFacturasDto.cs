using Farmacia_Api.Models;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos
{
    public class ReadDetalleFacturasDto
    {
        public string? nombreMed { get; set; }
        public string? nombreProd { get; set; }
        public decimal PreUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal? DescuentoObraSocial { get; set; }
        public bool Reembolsado { get; set; }

        [JsonIgnore]
        public ReadFacturaDto? FacturaDto { get; set; }
        //public ReadSucursalDto? SucursalDto { get; set; }
        public ReadMedicamentoDto? MedicamentoDto { get; set; }
    }
}