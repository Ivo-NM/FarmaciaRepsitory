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

        public async Task<bool> LoginUsuario(LoginUsuarioDto dto)
        {
            Usuario usuario = Mapeo(dto);
            return await repository.LoginUsuarioAsync(usuario);
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
