using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Farmacia_Api.Data.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly FarmaciaDbContext _context;
        public ClienteRepository(FarmaciaDbContext context)
        {
            _context = context;
        }

        public Task<bool> Actualizar(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AgregarAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}
