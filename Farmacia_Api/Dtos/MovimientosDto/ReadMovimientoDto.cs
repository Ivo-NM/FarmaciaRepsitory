using Farmacia_Api.Dtos.FacturasDto;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Dtos.OtrosProductosDto
{
    public class ReadMovimientoDto
    {
        public string? Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}