using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos
{
    public class CreateFacturaDto
    {
        public int NroSucursal { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }
        public virtual ICollection<CreateDetalleFacturasDto>? DetalleFacturasDtos { get; set; } = new List<CreateDetalleFacturasDto>();
    }
}
