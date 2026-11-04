namespace Farmacia_Api.Dtos.ClientesDto
{
    public class UpdateClienteDto
    {
        public string? Nombre { get; set; } = null!;
        public string? Apellido { get; set; } = null!;
        public int? Documento { get; set; }
        public string? Calle { get; set; }
        public int? Altura { get; set; }
        public int? IdBarrio { get; set; }
        public int? NroTelefono { get; set; }
        public string? Email { get; set; }
        public int? CodObraSocial { get; set; }
    }
}
