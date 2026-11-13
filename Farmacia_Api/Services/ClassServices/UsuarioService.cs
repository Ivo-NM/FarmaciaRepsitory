using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.UsuariosDto;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ReadLoginUsuarioDto?> LoginUsuario(LoginUsuarioDto dto)
        {
            Usuario usuario = Mapeo(dto);
            Usuario? usuarioEncontrado = await repository.LoginUsuarioAsync(usuario);

            if (usuarioEncontrado == null)
            {
                return null;
            }

            Empleado empleado = usuarioEncontrado.EmpleadoNavigation;

            return new ReadLoginUsuarioDto()
            {
                IdEmpleado = usuarioEncontrado.IdEmpleado,
                Correo = usuarioEncontrado.Correo,
                Nombre = empleado.NomEmpleado,
                Apellido = empleado.ApellEmpleado,
                Ocupacion = empleado.Ocupacion,
                NroSucursal = empleado.NroSucursal,
                NomSede = empleado.NroSucursalNavigation?.NomSede
            };
        }

        private Usuario Mapeo(LoginUsuarioDto dto)
        {
            Usuario usuario = new Usuario()
            {
                Contrasenia = dto.Contrasenia,
                Correo = dto.Correo
            };

            return usuario;
        }
    }
}
