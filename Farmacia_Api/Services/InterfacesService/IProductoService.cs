using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Models;
using System.Runtime.CompilerServices;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IProductoService
    {
        Task<OtrosProducto>? ObtenerProductoPorId(int id);
        Task<List<OtrosProducto>> ObtenerProductos();
    }
}
