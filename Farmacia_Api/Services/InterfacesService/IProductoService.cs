using Farmacia_Api.Dtos.OtrosProductosDto;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IProductoService
    {
        Task<List<ReadProductoDto?>> RecuperarProductosVendidosPorFecha(DateTime fechaInicio, DateTime fechaFin);
    }
}