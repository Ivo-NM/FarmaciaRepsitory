namespace Farmacia_Api.Dtos.EntregasDto
{
    public class CreateEntregaDto
    {
        public int NroSucursal { get; set; }
        public DateTime? Fecha { get; set; }
        public string FirmaEmpresa { get; set; }
        public string RespuestaEntrega { get; set; }
        public int IdEmpleado { get; set; }

        public IEnumerable<CreateDetalleEntregasDto> DetalleEntregasDtos { get; set; } = new List<CreateDetalleEntregasDto>();
    }
}
