using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.OtrosProductosDto;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class MovimientoRepository: IMovimientoRepository
    {
        private readonly FarmaciaDbContext context;

        public MovimientoRepository(FarmaciaDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Entrega?>> GetRecibidosByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return await context.Entregas
                          .Include(f => f.DetalleEntregas)
                          .ThenInclude(df => df.CodProductoNavigation)
                          .Include(f => f.DetalleEntregas)
                          .ThenInclude(df => df.CodMedicamentoNavigation)
                          .AsSplitQuery()
                         .Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFin)
                         .ToListAsync();
        }

        public async Task<List<Factura?>> GetVendidosByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return await context.Facturas
                          .Include(f => f.DetalleFacturas)
                          .ThenInclude(df => df.CodProductoNavigation)
                          .Include(f => f.DetalleFacturas)
                          .ThenInclude(df => df.CodMedicamentoNavigation)
                          .AsSplitQuery()
                         .Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFin)
                         .ToListAsync();
        }
    }
}