using Farmacia_Api.Dtos.InventariosDto;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IInventarioService
    {
        Task<IEnumerable<ReadInventarioDto>> ObtenerInventariosAsync();
        Task<IEnumerable<ReadInventarioDto>> ObtenerInventarioPorSucursalAsync(int id);
        Task<ReadInventarioDto?> ObtenerInventarioPorIdAsync(int id);
        Task<bool> CrearInventarioAsync(CreateInventarioDto inventario);

        //Metodos quye modifican el inventario
        Task<bool> RestarStockPorVentaAsync(int sucursalId, int insumoId, int cantidadVendida, bool esMedicamento);
        Task<bool> SumarStockPorEntregaAsync(int sucursalId, int insumoId, int cantidadRecibida, bool esMedicamento);

    }
}
