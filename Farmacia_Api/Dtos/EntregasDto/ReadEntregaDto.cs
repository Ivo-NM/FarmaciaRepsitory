namespace Farmacia_Api.Dtos.EntregasDto
{
    public class ReadEntregaDto
    {
        public int NroEntrega { get; set; }
        public DateTime? Fecha { get; set; }
        public string RespuestaEntrega { get; set; }
        public int IdEmpleado { get; set; }

        public IEnumerable<ReadDetalleEntregasDto> DetalleEntregasDtos { get; set; } = new List<ReadDetalleEntregasDto>();
    }
}
