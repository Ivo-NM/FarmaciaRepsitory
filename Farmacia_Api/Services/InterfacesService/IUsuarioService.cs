using Farmacia_Api.Dtos.UsuariosDto;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IUsuarioService
    {
        Task<bool> LoginUsuario(LoginUsuarioDto dto);
    }
}
