namespace Farmacia_Api.Dtos
{
    public class ReadFacturaDto
    {
        public int NroFactura { get; set; }
        public int NroSucursal { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }
        public ReadSucursalDto SucursalDto { get; set; }
        public ICollection<ReadDetalleFacturasDto> DetalleFacturasDto { get; set; } = new List<ReadDetalleFacturasDto>();
    }
}
