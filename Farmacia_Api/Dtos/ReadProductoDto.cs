using Farmacia_Api.Models;

namespace Farmacia_Api.Dtos
{
    public class ReadProductoDto
    {
        public int CodProducto { get; set; }
        public string Detalle { get; set; }
        public decimal PreUnitario { get; set; }
        public ICollection<ReadDetalleFacturasDto> DetalleFacturas { get; set; } = new List<ReadDetalleFacturasDto>();
    }
}