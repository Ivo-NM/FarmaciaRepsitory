namespace Farmacia_Api.Dtos.FacturasDto
{
    public class ReadFacturaDto
    {
        public int NroFactura { get; set; }
        public string? Nombsede { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEmpleado { get; set; }
        public string NomEmpleado { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }
        public string DetalleFormaPago { get; set; }
        public IEnumerable<ReadDetalleFacturasDto> DetalleFacturasDto { get; set; } = new List<ReadDetalleFacturasDto>();
    }
}
