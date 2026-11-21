using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly FarmaciaDbContext _context;
        public FacturaRepository(FarmaciaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Factura>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(f => f.NroSucursalNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodMedicamentoNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodProductoNavigation)
                .Include(f => f.IdEmpleadoNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Factura>>? GetAllInSedeAsync(int id)
        {
            return await _context.Facturas
                .Where(f => f.NroSucursal == id)
                .Include(f => f.NroSucursalNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodMedicamentoNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodProductoNavigation)
                .Include(f => f.IdEmpleadoNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.NroSucursalNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodMedicamentoNavigation)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(df => df.CodProductoNavigation)
                .Include(f => f.IdEmpleadoNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync(f => f.NroFactura == id);

        }

        public async Task<List<Factura?>> GetByDateAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Facturas
                        .Include(f => f.NroSucursalNavigation)
                        .Include(f => f.DetalleFacturas)
                        .ThenInclude(df => df.CodMedicamentoNavigation)
                        .Include(f => f.DetalleFacturas)
                        .ThenInclude(df => df.CodProductoNavigation)
                        .Include(f => f.IdEmpleadoNavigation)
                        .Include(f => f.IdFormaPagoNavigation)
                        .AsSplitQuery()
                        .Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFin)
                        .ToListAsync();
        }

        public async Task<Cliente>? GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);
        }
        public async Task<FormasDePago>? GetFormaDePagoByIdAsync(int id)
        {
            return await _context.FormasDePagos.FirstOrDefaultAsync(f => f.IdFormaPago == id);
        }
    }
}