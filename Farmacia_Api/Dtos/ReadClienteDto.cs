using Farmacia_Api.Models;

namespace Farmacia_Api.Dtos
{
    public class ReadClienteDto
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Documento { get; set; }
        public string? Calle { get; set; }
        public int? Altura { get; set; }
        public string? NroTelefono { get; set; }
        public string? EMail { get; set; }
        public Barrio Barrio { get; set; } = null!;
        public string? ObraSocialNombre { get; set; }
    }
}
