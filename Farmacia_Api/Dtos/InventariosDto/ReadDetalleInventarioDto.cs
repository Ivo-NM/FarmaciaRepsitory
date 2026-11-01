using Farmacia_Api.Models;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos.InventariosDto
{
    public class ReadDetalleInventarioDto
    {
        public int? CodMedicamento { get; set; }
        public string? NomMedicamento { get; set; }
        public int? CodProducto { get; set; }
        public string? NomProducto { get; set; }
        public int Stock { get; set; }
        [JsonIgnore]
        public virtual Medicamento? CodMedicamentoNavigation { get; set; }
        [JsonIgnore]
        public virtual OtrosProducto? CodProductoNavigation { get; set; }
        [JsonIgnore]
        public virtual Inventario? NroInventarioNavigation { get; set; }
    }
}
