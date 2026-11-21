using Farmacia_Api.Models;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos.InventariosDto
{
    public class ReadInventarioDto
    {
        public int NroInventario { get; set; }
        public string? NomSede { get; set; }
        public DateTime? FechaInventario { get; set; }
        public int IdEmpleado { get; set; }
        public string? NomEmpleado { get; set; }
        public virtual ICollection<ReadDetalleInventarioDto> DetalleInventarios { get; set; } = new List<ReadDetalleInventarioDto>();
        [JsonIgnore]
        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual Sucursale NroSucursalNavigation { get; set; } = null!;
    }
}
