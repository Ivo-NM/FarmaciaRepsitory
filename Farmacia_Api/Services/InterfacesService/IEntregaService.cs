using Farmacia_Api.Dtos.EntregasDto;
using Farmacia_Api.Models;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IEntregaService
    {
        Task<IEnumerable<ReadEntregaDto>> RecuperarEntregasAsync();
        Task<IEnumerable<ReadEntregaDto>> RecuperarEntregasPorSedeAsync(int id);
        Task<ReadEntregaDto?> RecuperarEntregaPorIdAsync(int id);
        Task<bool> CrearEntregaAsync(CreateEntregaDto entrega);
        Task<Sucursale>? GetSucursalesByIdAsync(int idSucursal);
        Task<Empleado>? GetEmpleadoByIdAsync(int idEmpleado);
        Task<Medicamento>? GetMedicamentoByIdAsync(int codMedicamento);
        Task<OtrosProducto>? GetProductoByIdAsync(int codProducto);
    }
}