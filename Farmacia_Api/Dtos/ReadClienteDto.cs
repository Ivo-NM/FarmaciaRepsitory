using Farmacia_Api.Models;

namespace Farmacia_Api.Dtos
{
    public class ReadClienteDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Documento { get; set; }
        public string? NroTelefono { get; set; }
        public string? Email { get; set; }
        public Barrio Barrio { get; set; } = null!;
        public string? ObraSocialNombre { get; set; }
    }
}
