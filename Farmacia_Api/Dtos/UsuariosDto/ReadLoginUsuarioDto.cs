namespace Farmacia_Api.Dtos.UsuariosDto
{
    public class ReadLoginUsuarioDto
    {
        public int IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Ocupacion { get; set; }
        public int NroSucursal { get; set; }
        public string? NomSede { get; set; }
    }
}
