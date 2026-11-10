using Farmacia_Api.Models;

namespace Farmacia_Api.Dtos.ClientesDto
{
    public class ReadClienteDto
    {
        public int idCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Documento { get; set; }
        public int? NroTelefono { get; set; }
        public string? Email { get; set; }
        public int? Id_barrio { get; set; }
        public string Barrio { get; set; } = null!;
        public string? calle { get; set; }
        public int? Altura { get; set; }
        public int? cod_ObraSocial { get; set; }
        public string? ObraSocialNombre { get; set; }
    }
}
