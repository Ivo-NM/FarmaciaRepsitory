using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly FarmaciaDbContext _context;

        public UsuarioRepository(FarmaciaDbContext _context)
        {
            this._context = _context;
        }

        public async Task<bool> LoginUsuarioAsync(Usuario usuario)
        {
            Usuario? usuarioEncontrado = await _context.Usuarios
                                     .FirstOrDefaultAsync(u => u.Correo == usuario.Correo &&
                                                          u.Contrasenia == usuario.Contrasenia);
        
            if (usuarioEncontrado == null)
            {
                return false;
            }

            return true;
        }
    }
}
