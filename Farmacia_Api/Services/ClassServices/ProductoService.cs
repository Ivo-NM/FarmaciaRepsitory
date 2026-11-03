using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Dtos.OtrosProductosDto;
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

        public async Task<List<ReadProductoDto?>> RecuperarProductosVendidosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin");
            }

            List<OtrosProducto>? lstProductos = await repository.GetVendidosByFecha(fechaInicio, fechaFin);
        
            if (lstProductos == null)
            {
                return null;
            }

            List<ReadProductoDto> lstProductoDto = Mapeo(lstProductos);
            return lstProductoDto;
        }

        public List<ReadProductoDto> Mapeo(List<OtrosProducto> productos)
        {
            List<ReadProductoDto> lstDto = new List<ReadProductoDto>();
            
            foreach (OtrosProducto p in productos)
            {
                ReadProductoDto productoDto = new ReadProductoDto()
                {
                    Detalle = p.Detalle,
                    Precio = p.PreUnitario
                };

                lstDto.Add(productoDto);
            }

            return lstDto;
        }
    }
}
