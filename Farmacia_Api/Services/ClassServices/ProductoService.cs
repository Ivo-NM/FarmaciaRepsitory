using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using Farmacia_Api.Services.InterfacesService;

namespace Farmacia_Api.Services.ClassServices
{
    public class ProductoService: IProductoService
    {
        private readonly IProductoRepository repository;

        public ProductoService(IProductoRepository repository)
        {
            this.repository = repository;
        }
        public async Task<OtrosProducto>? ObtenerProductoPorId(int id)
        {
            return await repository.GetProductoByIdAsync(id);
        }
    }
}
