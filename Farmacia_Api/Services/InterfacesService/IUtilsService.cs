using Farmacia_Api.Dtos.UtilsDto;

namespace Farmacia_Api.Services.InterfacesService
{
    public interface IUtilsService
    {
            Task<List<ReadBarrioDto>> TraerBarrios();
            Task<ReadBarrioDto> TraerBarrioPorId(int id);
            Task<List<ReadObraSocialDto>> TraerObraSocial();
            Task<ReadObraSocialDto> TraerObraSocialPorId(int id);
    }
}
