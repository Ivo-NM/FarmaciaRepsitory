using Farmacia_Api.Models;

namespace Farmacia_Api.Dtos.ClientesDto
{
    public class ReadClienteDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Documento { get; set; }
        public int? NroTelefono { get; set; }
        public string? Email { get; set; }
        public string Barrio { get; set; } = null!;
        public string? ObraSocialNombre { get; set; }
    }
}
