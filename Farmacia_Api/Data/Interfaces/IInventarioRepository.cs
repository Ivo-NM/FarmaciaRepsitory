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
        Task<DetalleInventario?> VerificarStockDisponibleAsync(int sucursalId, int insumoId, int cantidadRequerida, bool esMedicamento);
        Task<bool> ModificarStockAsync(DetalleInventario detalleInventario, int cantidad, bool esResta);
    }
}
