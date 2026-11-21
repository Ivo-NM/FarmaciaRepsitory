using Farmacia_Api.Dtos.OtrosProductosDto;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IMovimientoService
    {
        Task<List<ReadMovimientoDto?>> RecuperarProductosVendidosPorFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<List<ReadMovimientoDto?>> RecuperarProductosRecibidosPorFecha(DateTime fechaInicio, DateTime fechaFin);

    }
}