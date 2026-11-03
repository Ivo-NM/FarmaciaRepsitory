using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data.Implementations
{
    public class ProductoRepository: IProductoRepository
    {
        private readonly FarmaciaDbContext context;

        public ProductoRepository(FarmaciaDbContext context)
        {
            this.context = context;
        }

        public async Task<List<OtrosProducto?>> GetVendidosByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return await context.OtrosProductos
                         .Where(p => p.DetalleFacturas.Any(df => df.NroFacturaNavigation.Fecha >= fechaInicio &&
                         df.NroFacturaNavigation.Fecha <= fechaFin))
                         .ToListAsync();
        }
    }
}