using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IProductoService
    {
        Task<List<OtrosProducto?>> RecuperarProductoPorMes(int mesInicio, int mesFin);
    }
}
