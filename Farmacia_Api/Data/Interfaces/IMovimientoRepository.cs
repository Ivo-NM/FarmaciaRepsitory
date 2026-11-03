using Farmacia_Api.Dtos.OtrosProductosDto;
using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IMovimientoRepository
    {
        Task<List<Factura?>> GetVendidosByFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<List<Entrega?>> GetRecibidosByFecha(DateTime fechaInicio, DateTime fechaFin);

    }
}
