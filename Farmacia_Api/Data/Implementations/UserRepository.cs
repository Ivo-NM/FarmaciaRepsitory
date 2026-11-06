using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.UsuarioDto;

namespace Farmacia_Api.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly FarmaciaDbContext _context;
        public UserRepository(FarmaciaDbContext context)
        {
            _context = context;
        }
        public bool login(UserLoginDto usuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.correo == usuario.Email && u.contrasenia == usuario.Password) != null;
        }
    }
}
