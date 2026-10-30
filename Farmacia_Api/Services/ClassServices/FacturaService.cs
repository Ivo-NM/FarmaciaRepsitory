using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class FacturaService: IFacturaService
    {
        private readonly IFacturaRepository repository;

        public FacturaService(IFacturaRepository repository)
        {
            this.repository = repository;
        }

        public Task<bool> CrearFacturaAsync(Factura factura)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Factura>> ObtenerFacturasAsync()
        {
            return await repository.GetAllAsync();
        }

        public Task<Factura?> ObtenerFacturasPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Factura>> ObtenerFacturasPorSucursalAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
