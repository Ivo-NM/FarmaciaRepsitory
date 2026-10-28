using Farmacia_Api.Models;

namespace Farmacia_Api.Data.Interfaces
{
    public interface IInventarioRepository
    {
        // Operaciones CRUD
        Task<IEnumerable<Inventario>> GetAllAsync();
        Task<IEnumerable<Inventario>> GetByInSedeAsync(int id);
        Task<Inventario?> GetByIdAsync(int id);
        Task<bool> AddAsync(Inventario inventario);
        Task<bool> UpdateAsync(Inventario inventario);

        // Movimientos y control de stock
        Task<bool> VerificarStockDisponibleAsync(int sucursalId, int InsumoId, int cantidadRequerida);
        Task<bool> RestarStockPorVentaAsync(int sucursalId, int InsumoId, int cantidadVendida);
        Task<bool> SumarStockPorEntregaAsync(int sucursalId, int InsumoId, int cantidadRecibida);
    }
}
