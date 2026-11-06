using Farmacia_Api.Dtos.UsuarioDto;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IUserRepository
    {
        bool login(UserLoginDto usuario);
    }
}
