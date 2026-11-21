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

        public async Task<Usuario?> LoginUsuarioAsync(Usuario usuario)
        {
            return await _context.Usuarios
                                 .Include(u => u.EmpleadoNavigation)
                                    .ThenInclude(e => e.NroSucursalNavigation)
                                 .FirstOrDefaultAsync(u => u.Correo == usuario.Correo &&
                                                      u.Contrasenia == usuario.Contrasenia);
        }
    }
}
